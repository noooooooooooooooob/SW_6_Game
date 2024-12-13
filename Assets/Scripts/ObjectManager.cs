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
    public List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    int spawnIndex = 0;
    public int inputIndex = 0;

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
            timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
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
                SetNoteColor(true, 0);
                SetNoteDirection(true, 0);
                SetNoteLine(true, 0);
                SetNoteAttribute();
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }
    }

    public void SetNoteLine(bool isRandom, int line)
    {
        if (isRandom)
        {
            notes[spawnIndex].GetComponent<Note>().spawnLine = UnityEngine.Random.Range(0, floors + 1);
        }
        else
        {
            notes[spawnIndex].GetComponent<Note>().spawnLine = line;
        }
    }

    public void SetNoteColor(bool isRandom, int setColor)
    {
        if (isRandom)
        {
            notes[spawnIndex].GetComponent<Note>().coloridx = UnityEngine.Random.Range(0, 3);
        }
        else
        {
            notes[spawnIndex].GetComponent<Note>().coloridx = setColor;
        }

        // notePatterns.patternNoteColoridx = note[cnt].GetComponent<Note>().coloridx;
        // notePatterns.patternNoteColoridx = notes[spawnIndex].GetComponent<Note>().coloridx;
    }

    public void SetNoteDirection(bool isRandom, int setDirection)
    {
        if (isRandom)
        {
            notes[spawnIndex].GetComponent<Note>().arrowidx = UnityEngine.Random.Range(0, 4);
        }
        else
        {
            notes[spawnIndex].GetComponent<Note>().arrowidx = setDirection;
        }

        // notePatterns.patternNoteArrowidx = note[cnt].GetComponent<Note>().arrowidx;
        // notePatterns.patternNoteArrowidx = notes[spawnIndex].GetComponent<Note>().arrowidx;
    }

    public void SetNoteAttribute()
    {

        if (isSlow)
            notes[spawnIndex].GetComponent<Note>().speed *= 0.5f;
        if (oppositeNoteArrow)
            notes[spawnIndex].GetComponent<Note>().isOpposite = true;
        if (fadeNote)
            notes[spawnIndex].GetComponent<Note>().isFaded = true;
        if (unifyNote)
        {
            GameObject[] activeNote = GameObject.FindGameObjectsWithTag("Note");

            foreach (GameObject n in activeNote)
            {
                n.GetComponent<Note>().isSame = true;
            }
            notes[spawnIndex].GetComponent<Note>().isSame = true;
        }
    }

    public ArrowDirectionEnum GetCurrentNoteArrowDirection(int currentNote)
    {
        if (notes.Count > 0)
        {
            return notes[currentNote].GetComponent<Note>().noteArrowDirection;
        }
        return 0;
    }

    public ColorEnum getCurrentNoteColor(int currentNote)
    {
        if (notes.Count > 0)
        {
            return notes[currentNote].GetComponent<Note>().noteColor;
        }
        return 0;
    }

    public int getCurrentNoteLine(int currentNote)
    {
        if (notes.Count > 0)
        {
            return notes[currentNote].GetComponent<Note>().spawnLine;
        }
        return 0;
    }

    public bool isDestroyed(int currentNote)
    {
        if (notes.Count > 0)
        {
            return notes[currentNote].GetComponent<Note>().isDestroyed;
        }
        return false;
    }

    public void PlayHitSound(int currentNote)
    {
        if (notes.Count > 0)
        {
            notes[currentNote].GetComponent<Note>().playNoteHitSound();
        }
    }

    public void MoveToBoss(int currentNote)
    {
        if (notes.Count > 0)
        {
            notes[currentNote].GetComponent<Note>().StartMovingToBoss();
        }
    }

    public void DestroyNote(int index)
    {
        Destroy(notes[index].gameObject);
    }
}

