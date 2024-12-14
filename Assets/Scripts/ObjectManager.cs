using Melanchall.DryWetMidi.Interaction;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectManager : MonoBehaviour
{
    public GameObject notePrefab;
    private NotePatterns notePatterns;

    //Gimmicks
    public bool isSlow;
    public bool changingNoteLocation;
    public bool oppositeNoteArrow;
    public bool fadeNote;
    public bool unifyNote;


    [Range(0, 2)]
    public int floors;
    //midi
    public Melanchall.DryWetMidi.MusicTheory.NoteName[] noteRestriction;
    public List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    int spawnIndex = 0;
    public int inputIndex = 0;
    public List<int> noteKeys = new List<int>();

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        int i = 0;
        foreach (var note in array)
        {

            if (note.NoteName == noteRestriction[0])
            {
                noteKeys.Add(0);
            }
            else if (note.NoteName == noteRestriction[1])
            {
                noteKeys.Add(1);
            }
            else if (note.NoteName == noteRestriction[2])
            {
                noteKeys.Add(2);
            }

            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
            timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);

            i++;
        }
    }

    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());

                if (noteKeys[spawnIndex] == 0) //Normal notes
                {
                    SetNoteDirection(true, 0);
                    SetNoteColor(true, 0);
                    SetNoteLine(true, 0);
                    SetNoteAttribute();
                }
                else //pattern notes
                {
                    if (noteKeys[spawnIndex] == noteKeys[spawnIndex - 1])
                    { // same level
                        SetNoteColor(false, notes[spawnIndex - 1].coloridx);
                        SetNoteDirection(true, 0);
                        SetNoteLine(false, notes[spawnIndex - 1].spawnLine);
                        SetNoteAttribute();
                    }
                    else
                    {
                        SetNoteColor(true, 0);
                        SetNoteDirection(true, 0);
                        SetNoteLine(true, 0);
                        SetNoteAttribute();
                    }

                }
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }
    }

    public void SetNoteLine(bool isRandom, int line)
    {
        if (isRandom)
        {
            notes[spawnIndex].spawnLine = UnityEngine.Random.Range(0, floors + 1);
        }
        else
        {
            notes[spawnIndex].spawnLine = line;
        }
    }

    public void SetNoteColor(bool isRandom, int setColor)
    {
        if (isRandom)
        {
            notes[spawnIndex].coloridx = UnityEngine.Random.Range(0, 3);
        }
        else
        {
            notes[spawnIndex].coloridx = setColor;
        }

        // notePatterns.patternNoteColoridx = note[cnt].GetComponent<Note>().coloridx;
        // notePatterns.patternNoteColoridx = notes[spawnIndex].GetComponent<Note>().coloridx;
    }

    public void SetNoteDirection(bool isRandom, int setDirection)
    {
        if (isRandom)
        {
            notes[spawnIndex].arrowidx = UnityEngine.Random.Range(0, 4);
        }
        else
        {
            notes[spawnIndex].arrowidx = setDirection;
        }

        // notePatterns.patternNoteArrowidx = note[cnt].GetComponent<Note>().arrowidx;
        // notePatterns.patternNoteArrowidx = notes[spawnIndex].GetComponent<Note>().arrowidx;
    }

    public void SetNoteAttribute()
    {

        if (isSlow)
            notes[spawnIndex].speed *= 0.5f;
        if (oppositeNoteArrow)
            notes[spawnIndex].isOpposite = true;
        if (fadeNote)
            notes[spawnIndex].isFaded = true;
        if (unifyNote)
        {
            GameObject[] activeNote = GameObject.FindGameObjectsWithTag("Note");

            foreach (GameObject n in activeNote)
            {
                n.GetComponent<Note>().isSame = true;
            }
            notes[spawnIndex].isSame = true;
        }
    }

    public ArrowDirectionEnum GetCurrentNoteArrowDirection(int currentNote)
    {
        if (notes.Count > currentNote && currentNote > 0)
        {
            return notes[currentNote].noteArrowDirection;
        }
        return 0;
    }

    public ColorEnum getCurrentNoteColor(int currentNote)
    {
        if (notes.Count > currentNote && currentNote > 0)
        {
            return notes[currentNote].noteColor;
        }
        return 0;
    }

    public int getCurrentNoteLine(int currentNote)
    {
        if (notes.Count > currentNote && currentNote > 0)
        {
            return notes[currentNote].spawnLine;
        }
        return 0;
    }

    public bool isDestroyed(int currentNote)
    {
        if (notes.Count > currentNote && currentNote > 0)
        {
            return notes[currentNote].isDestroyed;
        }
        return false;
    }

    public void PlayHitSound(int currentNote)
    {
        if (notes.Count > currentNote && currentNote > 0)
        {
            notes[currentNote].playNoteHitSound();
        }
    }

    public void MoveToBoss(int currentNote)
    {
        if (notes.Count > currentNote && currentNote > 0)
        {
            notes[currentNote].StartMovingToBoss();
        }
    }

    public void DestroyNote(int index)
    {
        Destroy(notes[index].gameObject);
    }

    public string returnNoteName(int currentNote)
    {
        if (notes.Count > currentNote && currentNote > 0)
        {
            return noteKeys[currentNote].ToString();
        }
        return null;
    }
}

