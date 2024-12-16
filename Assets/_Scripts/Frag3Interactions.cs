using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frag3Interactions : MonoBehaviour
{
    [SerializeField] private GameObject crystalObject; // Object to hide
    [SerializeField] private GameObject journalObject; // Object to show
    [SerializeField] private GameObject thisImageTarget; // Parent Image target of the this gameobject

    [SerializeField] private AudioClip firstSound; // First sound clip to play
    [SerializeField] private AudioClip interactionSound; // Sound to play

    [SerializeField] private GameObject spider; // New spider object to hide
    [SerializeField] private float shakeThreshold = 2.0f; // Threshold for shake detection

    private GameProgressManager progressManager;
    private AudioSource audioSource; // Single AudioSource component
    private bool spiderRemoved = false; // Track if spider is already gone

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Configure the AudioSource default settings
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Find the GameProgressManager in the scene
        progressManager = FindObjectOfType<GameProgressManager>();

        if (journalObject != null)
            journalObject.SetActive(false);
    }

    void Update()
    {
        // Check for shake first
        if (!spiderRemoved && Input.acceleration.magnitude > shakeThreshold)
        {
            spider.SetActive(false);
            Debug.Log("Spider removed by shaking");
            spiderRemoved = true;
        }

        // FOR DEBUGGING PURPOSES ONLY
        if (!spiderRemoved && Input.GetKey(KeyCode.A))
        {
            spider.SetActive(false);
            Debug.Log("Spider removed by shaking");
            spiderRemoved = true;
        }

        if (spiderRemoved && Input.GetMouseButtonDown(0))
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
            // Check if the hit object is this fragment or its child
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {
                // Check if this fragment is the currently active one
                if (progressManager != null && progressManager.IsFragmentActive(thisImageTarget))
                {
                    ProcessSequence();
                }
                else
                {
                    Debug.Log($"{gameObject.name} is not active yet.");
                }
            }
        }
    }

    private void ProcessSequence()
    {
        if (crystalObject != null)
        {
            crystalObject.SetActive(false);
        }

        if (journalObject != null)
        {
            journalObject.SetActive(true);
        }

        // Start the audio sequence
        StartCoroutine(PlayAudioSequence());
    }

    private IEnumerator PlayAudioSequence()
    {
        // Ensure the AudioSource is assigned and initialized
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            yield break;
        }

        // Play the first sound
        PlaySound(interactionSound);
        Debug.Log("Crystal hidden, journal shown");

        // Wait for the first sound to finish
        yield return new WaitForSeconds(interactionSound.length);

        // Play the second sound (firstSound)
        PlaySound(firstSound);
        Debug.Log("First sound played");

        // Proceed with unlocking the next fragment
        float totalAudioLength = interactionSound.length + firstSound.length + 1;  // Add 1 second buffer if needed
        StartCoroutine(DelayedUnlockNextFragment(totalAudioLength));
    }

    private IEnumerator DelayedUnlockNextFragment(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        if (progressManager != null)
        {
            progressManager.UnlockNextFragment();
            Debug.Log("Fragment collected and next unlocked");
        }
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
