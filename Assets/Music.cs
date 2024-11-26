using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource music; // The AudioSource playing the music
    public GameObject notePrefab; // Prefab for the note
    public Transform spawnPoint; // Where the notes should spawn

    public float sensitivity = 1.5f; // Sensitivity for beat detection
    private float[] spectrum = new float[256];
    private float lastSpawnTime = 0f;

    void Update()
    {
        music.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        // Sum up low-frequency bands (e.g., first 10 bands for bass/beat detection)
        float average = 0f;
        for (int i = 0; i < 10; i++)
        {
            // Debug.Log(spectrum[i]);
            average += spectrum[i];
        }

        // Spawn a note if the average exceeds a threshold
        if (average > sensitivity && Time.time - lastSpawnTime > 0.3f)
        {
            SpawnNote();
            lastSpawnTime = Time.time; // Avoid spawning too frequently
        }
    }

    void SpawnNote()
    {
        Debug.Log("Spawn note!");
        // Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
    }

}
