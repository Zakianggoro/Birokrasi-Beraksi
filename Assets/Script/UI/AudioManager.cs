using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    private static AmbientAudio instance;

    [SerializeField] private AudioClip ambientClip; // Assign this in the Inspector

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this as the instance and persist across scenes
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Add AudioSource component if it doesn't exist and configure it
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = ambientClip;
        audioSource.loop = true;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}