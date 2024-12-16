using System.Collections;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [SerializeField] GameObject textObject; // The text to hide first
    [SerializeField] GameObject thisImageTarget; // the parent ImageTarget
    [SerializeField] AudioClip firstSound; // First sound clip to play
    [SerializeField] AudioClip secondSound; // Second sound clip to play
    [SerializeField] KeyCode testKey = KeyCode.Space;
    [SerializeField] GameProgressManager progressManager;
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
            if (progressManager != null && progressManager.IsFragmentActive(thisImageTarget))
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
    }

    private void ProcessSequence()
    {
        switch (interactionCount)
        {
            case 0: // First interaction
                if (textObject != null)
                {
                    textObject.SetActive(false);
                    Debug.Log("Text deactivated");
                }
                PlaySound(firstSound);
                Debug.Log("Playing first sound");
                break;

            case 1: // Second interaction
                PlaySound(secondSound);
                Debug.Log("Playing second sound");
                StartCoroutine(DelayedUnlockNextFragment(secondSound.length));
                break;
        }

        //if (interactionCount < 4)
        //{
        //    interactionCount++;
        //}
        interactionCount++;
    }

    private IEnumerator DelayedUnlockNextFragment(float delay)
    {
        delay = delay + 1;
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
