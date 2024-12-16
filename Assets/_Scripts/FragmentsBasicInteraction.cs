using System.Collections;
using UnityEngine;

public class FragmentsBasicInteraction : MonoBehaviour
{
    [SerializeField] GameObject crystalObject; // Object to hide
    [SerializeField] GameObject journalObject; // Object to show
    [SerializeField] GameObject thisImageTarget; // Parent Image target of the this gameobject
    [SerializeField] AudioClip firstSound; // First sound clip to play
    [SerializeField] AudioClip interactionSound; // Sound to play
    [SerializeField] KeyCode testKey = KeyCode.Space;

    private GameProgressManager progressManager;
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
        if (isTest)
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
        else
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
    }

    private void ProcessSequence()
    {
        if (crystalObject != null)
            crystalObject.SetActive(false);
        if (journalObject != null)
            journalObject.SetActive(true);
        PlaySound(interactionSound);
        PlaySound(firstSound);
        Debug.Log("Crystal hidden, journal shown");

        float audioClipsLength = interactionSound.length + firstSound.length;

        StartCoroutine(DelayedUnlockNextFragment(audioClipsLength));
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
