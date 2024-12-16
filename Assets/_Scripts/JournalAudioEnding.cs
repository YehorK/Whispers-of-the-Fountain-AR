using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalAudioEnding : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component
    private bool hasAudioPlayed = false; // Flag to ensure the audio is played only once

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject!");
        }
    }

    public void PlayAudio()
    {
        // Check if the audio has already been played
        if (!hasAudioPlayed && audioSource != null)
        {
            audioSource.Play();
            hasAudioPlayed = true; // Mark the audio as played
        }
    }
}
