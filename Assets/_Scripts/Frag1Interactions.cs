using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frag1Interactions : MonoBehaviour
{
    public GameObject targetObject; // The object to toggle visibility
    public GameObject crystalObject; // Object to hide
    public GameObject journalObject; // Object to show
    public AudioClip firstSound; // First sound clip to play
    public AudioClip interactionSound; // Sound to play
    public KeyCode testKey = KeyCode.Space;
    private GameProgressManager progressManager;
    private int interactionCount = 0;
    private AudioSource audioSource; // Single AudioSource component

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
        // Check for screen tap
        if (Input.GetMouseButtonDown(0)) // Touch or left mouse button
        {
            HandleInteraction();
        }

        // Allow testing with the space key
        if (Input.GetKeyDown(testKey))
        {
            HandleInteraction(true);
        }
    }

    private void HandleInteraction(bool isTest = false)
    {
        if (isTest) {
            // Check if this fragment is the currently active one
            if (progressManager != null && progressManager.IsFragmentActive(gameObject))
            {
                ProcessSequence();
            }
            else
            {
                Debug.Log($"{gameObject.name} is not active yet.");
            }
        } else {
            // Cast a ray from the camera to the screen tap position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the hit object is this fragment or its child
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    // Check if this fragment is the currently active one
                    if (progressManager != null && progressManager.IsFragmentActive(gameObject))
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
    }

    private void ProcessSequence()
    {
        switch (interactionCount)
        {
            case 0: // First interaction
                if (crystalObject != null)
                    crystalObject.SetActive(false);
                if (journalObject != null)
                    journalObject.SetActive(true);
                PlaySound(interactionSound);
                PlaySound(firstSound);
                Debug.Log("Crystal hidden, journal shown");
                break;
            case 1:
                CollectFragment();
                progressManager.UnlockNextFragment();
                Debug.Log("Fragment collected and next unlocked");
                break;
        }

        if (interactionCount < 4)
        {
            interactionCount++;
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

    private void CollectFragment()
    {
        // Hide or deactivate the target object
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
