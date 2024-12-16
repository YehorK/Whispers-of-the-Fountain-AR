using System;
using System.Collections; // Required for IEnumerator and coroutines
using UnityEngine;

public class FadeOgopogo : MonoBehaviour
{
    [SerializeField] public GameObject targetObject; // Object to fade
    [SerializeField] public float fadeDuration = 1.0f; // Duration of the fade effect
    [SerializeField] public bool fadeOut = true; // Set true for fade-out, false for fade-in

    public AudioSource audioSource; // AudioSource component
    private bool fadedOut = false;
    private void Start()
    {
        // Ensure the AudioSource component is attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to this GameObject!");
        }
    }

    public void Update()
    {
        if (!fadedOut)
        {
            StartFading();
        }
    }

    public void StartFading()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        // Ensure the object is active before starting the fade process
        if (!targetObject.activeInHierarchy)
        {
            targetObject.SetActive(true);
            Debug.Log("Target object activated!");
        }

        // Start checking for when the audio stops playing
        StartCoroutine(WaitForSoundToEnd());
    }

    private IEnumerator WaitForSoundToEnd()
    {
        // Wait until the AudioSource stops playing
        while (audioSource.isPlaying)
        {
            // Debug.Log("Audio is playing...");
            yield return null; // Wait for the next frame
        }

        // If AudioSource stops playing, proceed to fading
        Debug.Log("Audio finished playing, starting fade...");
        Renderer[] objectRenderers = targetObject.GetComponentsInChildren<Renderer>();
        if (objectRenderers.Length > 0)
        {
            foreach (Renderer objectRenderer in objectRenderers)
            {
                // Debug.Log("Fading effect on Renderer: " + objectRenderer.name);
                StartCoroutine(FadeObject(objectRenderer, fadeOut));
            }
        }
        else
        {
            Debug.LogError("Target object does not have any Renderer components!");
        }
    }

    private IEnumerator FadeObject(Renderer objectRenderer, bool isFadingOut)
    {
        float elapsedTime = 0f;

        // For each material on the renderer, fade the color
        foreach (Material material in objectRenderer.materials)
        {
            Color initialColor = material.color;

            // Determine start and end alpha values
            float startAlpha = isFadingOut ? initialColor.a : 1f;
            float endAlpha = isFadingOut ? 0f : 1f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

                // Update material color
                Color newColor = material.color;
                newColor.a = alpha;
                material.color = newColor;

                yield return null;
            }

            // Ensure the final alpha is set
            Color finalColor = material.color;
            finalColor.a = endAlpha;
            material.color = finalColor;
        }

        fadedOut = true; //avoid the script running again.

        // Optionally, deactivate the object when fade-out completes
        if (isFadingOut)
        {
            targetObject.SetActive(false);
            Debug.Log("Target object deactivated after fade-out.");
        }
    }
}
