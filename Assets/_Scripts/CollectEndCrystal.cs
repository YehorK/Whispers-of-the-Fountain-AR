using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEndCrystal : MonoBehaviour
{
    [SerializeField] private AudioClip interactionSound; // Sound to play
    [SerializeField] private AudioSource audioSource; // AudioSource to play the sound
    [SerializeField] private GameObject[] CrystalObjects; // List of objects to deactivate

    void Update()
    {
        // Check for screen tap
        if (Input.GetMouseButtonDown(0)) // Touch or left mouse button
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        // Cast a ray from the camera to the screen tap position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the hit object matches any crystal object
            foreach (GameObject crystalObject in CrystalObjects)
            {
                if (hit.transform.gameObject == crystalObject)
                {
                    // Deactivate the crystal object
                    Destroy(crystalObject);
                    
                    // Play the interaction sound
                    StartCoroutine(PlayAudioSequence());
                    break; // Exit the loop after handling the hit
                }
            }
        }
    }

    private IEnumerator PlayAudioSequence()
    {
        // Ensure the AudioSource is assigned and initialized
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            yield break;
        }

        // Play the interaction sound
        PlaySound(interactionSound);

        // Wait for the sound to finish
        if (interactionSound != null)
        {
            yield return new WaitForSeconds(interactionSound.length);
        }

        Debug.Log("Crystal interaction completed.");
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
