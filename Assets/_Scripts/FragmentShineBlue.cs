using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentShineBlue : MonoBehaviour
{
    [SerializeField] private float shineDuration = 1f;   // Duration of the shine (in seconds)
    [SerializeField] private Color shineColor = Color.blue;  // The color the crystal will shine with
    [SerializeField] private float shineIntensity = 5f;   // How intense the shine should be
    private Material crystalMaterial;
    private float currentEmissionIntensity = 0f;
    private bool isShining = false;
    private float shineSpeed;

    void Start()
    {
        // Get the material of the crystal object
        Renderer crystalRenderer = GetComponent<Renderer>();
        if (crystalRenderer != null)
        {
            crystalMaterial = crystalRenderer.material;
            crystalMaterial.EnableKeyword("_EMISSION"); // Ensure emission is enabled
        }
        else
        {
            Debug.LogError("No Renderer found on crystal.");
        }

        // Calculate shine speed (this is how quickly it will transition between 0 and full intensity)
        shineSpeed = shineIntensity / shineDuration;

        // Start the periodic shine effect
        Debug.Log("Getting to invokerepeating");
        InvokeRepeating("ToggleShine", 0f, shineDuration);  // Toggle shine every 'shineDuration' seconds
    }

    void Update()
    {
        // Smoothly transition the emission intensity based on whether we are shining or not
        float targetIntensity = isShining ? shineIntensity : 0f;
        currentEmissionIntensity = Mathf.MoveTowards(currentEmissionIntensity, targetIntensity, shineSpeed * Time.deltaTime);

        // Apply the emission color with the interpolated intensity
        crystalMaterial.SetColor("_EmissionColor", shineColor * currentEmissionIntensity);
    }

    // This method toggles the shining effect
    private void ToggleShine()
    {
        Debug.Log("shining");
        // Flip the shine state
        isShining = !isShining;
    }

    //void OnDisable()
    //{
    //    // Clean up by stopping the periodic calls when the object is disabled
    //    CancelInvoke();
    //}
}
