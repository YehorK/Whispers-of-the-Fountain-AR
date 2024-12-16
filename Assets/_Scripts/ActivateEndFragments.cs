using UnityEngine;

public class ActivateEndFragments : MonoBehaviour
{
    public AudioSource audioSource; // Assign the AudioSource in the Inspector
    public GameObject[] objectsToActivate; // List of objects to activate

    private bool hasActivated = false; // To ensure activation happens only once

    void Update()
    {
        // Check if the audio has stopped playing and activation hasn't occurred yet
        if (!audioSource.isPlaying && !hasActivated)
        {
            ActivateObjects();
            hasActivated = true; // Prevent multiple activations
        }
    }

    void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true); // Activate each object in the array
        }
    }
}
