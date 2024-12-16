using System.Collections;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [SerializeField] GameObject textObject; // The text to hide first
    [SerializeField] GameObject thisImageTarget; // the parent ImageTarget
    [SerializeField] AudioClip firstSound; // First sound clip to play
    [SerializeField] AudioClip secondSound; // Second sound clip to play

    [SerializeField] GameProgressManager progressManager;

    private bool hasInteracted = false;
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
        if (!hasInteracted && Input.GetMouseButtonDown(0)) // Touch or left mouse button
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
                    hasInteracted |= true;
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
        {
            textObject.SetActive(false);
            Debug.Log("Text deactivated");
        }

        StartCoroutine(PlayAudioSequence());
    }

    private IEnumerator PlayAudioSequence()
    {
        // Play the first sound
        PlaySound(firstSound);
        Debug.Log("Playing first sound");

        // Play the first sound
        audioSource.clip = firstSound;
        audioSource.PlayDelayed(0); // Ensures the AudioSource is ready before playing
        Debug.Log("Playing first sound");

        // Wait for the first sound to finish
        yield return new WaitForSeconds(firstSound.length + 1);

        // Play the second sound
        PlaySound(secondSound);
        Debug.Log("Playing second sound");

        // Unlock the next fragment or perform other actions
        StartCoroutine(DelayedUnlockNextFragment(secondSound.length));
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
           // audioSource.clip = clip;
            audioSource.PlayOneShot(clip);
        }
    }
}
