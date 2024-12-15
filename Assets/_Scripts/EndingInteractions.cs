using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndingInteractions : MonoBehaviour
{
    public GameObject journalObject;
    public GameObject[] crystals;
    public TMP_Text instructionText;
    public GameObject ogopogo;
    public AudioClip journalSound;
    public AudioClip collectionSound;
    public AudioClip finalSound;
    public KeyCode testKey = KeyCode.Space;
    public float fadeInDuration = 2f;
    
    private GameProgressManager progressManager;
    private AudioSource audioSource;
    private bool journalInteractionComplete = false;
    private int crystalsCollected = 0;
    private Material ogopogoMaterial;
    private bool isFading = false;
    private bool journalSoundPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        progressManager = FindObjectOfType<GameProgressManager>();

        // Initialize state
        foreach (GameObject crystal in crystals)
            crystal.SetActive(false);
        
        if (instructionText != null)
            instructionText.gameObject.SetActive(false);
            
        if (ogopogo != null)
        {
            ogopogo.SetActive(false);
            if (ogopogo.TryGetComponent<Renderer>(out var renderer))
                ogopogoMaterial = renderer.material;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleInteraction();
        
        if (Input.GetKeyDown(testKey))
            HandleInteraction(true);
    }

    private void HandleInteraction(bool isTest = false)
    {
        if (!progressManager.IsFragmentActive(gameObject)) return;

        if (isTest)
        {
            if (!journalInteractionComplete)
            {
                PlayJournalSequence();
                return;
            }
            if (crystalsCollected < crystals.Length)
            {
                HandleCrystalTap(crystals[crystalsCollected]);
                return;
            }
            if (crystalsCollected >= crystals.Length && !isFading)
            {
                PlayFinalSound();
                return;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        if (!journalInteractionComplete && hit.transform.gameObject == journalObject)
        {
            PlayJournalSequence();
            return;
        }

        if (journalInteractionComplete)
        {
            foreach (GameObject crystal in crystals)
            {
                if (hit.transform.gameObject == crystal)
                {
                    HandleCrystalTap(crystal);
                    return;
                }
            }

            if (crystalsCollected >= crystals.Length && hit.transform.gameObject == ogopogo && !isFading)
            {
                PlayFinalSound();
            }
        }
    }

    private void PlayJournalSequence()
    {
        if (journalSoundPlaying) return;
        
        PlaySound(journalSound);
        journalSoundPlaying = true;
        StartCoroutine(WaitForJournalSound());
    }

    private IEnumerator WaitForJournalSound()
    {
        yield return new WaitForSeconds(3f);
        journalSoundPlaying = false;
        journalObject.SetActive(false);
        journalInteractionComplete = true;
        
        foreach (GameObject crystal in crystals)
            crystal.SetActive(true);
        
        if (instructionText != null)
            instructionText.gameObject.SetActive(true);
    }

    private void HandleCrystalTap(GameObject crystal)
    {
        if (!crystal.activeSelf) return;
        
        crystal.SetActive(false);
        PlaySound(collectionSound);
        crystalsCollected++;

        if (crystalsCollected >= crystals.Length)
        {
            // Hide instruction text
            if (instructionText != null)
                instructionText.gameObject.SetActive(false);
                
            // Start Ogopogo sequence
            ogopogo.SetActive(true);
            StartCoroutine(FadeInOgopogo());
            PlayFinalSound();
        }
    }

    private void PlayFinalSound()
    {
        PlaySound(finalSound);
        progressManager.UnlockNextFragment();
    }

    private IEnumerator FadeInOgopogo()
    {
        if (ogopogo == null || ogopogoMaterial == null) yield break;

        isFading = true;

        // Ensure Ogopogo is visible but fully transparent
        ogopogo.SetActive(true);
        Color startColor = ogopogoMaterial.color;
        startColor.a = 0f;
        ogopogoMaterial.color = startColor;

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            Color newColor = ogopogoMaterial.color;
            newColor.a = alpha;
            ogopogoMaterial.color = newColor;
            yield return null;
        }

        // Ensure final opacity is exactly 1
        Color finalColor = ogopogoMaterial.color;
        finalColor.a = 1f;
        ogopogoMaterial.color = finalColor;
        
        isFading = false;
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
