using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentCollected : MonoBehaviour
{
    public GameObject targetObject; // The object to toggle visibility
    public KeyCode testKey = KeyCode.Space; // Key for testing on PC

    void Update()
    {
        // Check for screen tap or mouse click
        if (Input.GetMouseButtonDown(0)) // For touch devices or left mouse button
        {
            HandleInteraction();
        }

        // Allow testing with a keyboard key
        if (Input.GetKeyDown(testKey))
        {
            Debug.Log("Testing fragment collection via key press.");
            CollectFragment();
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
                Debug.Log($"{gameObject.name} tapped.");
                CollectFragment();
            }
        }
    }

    private void CollectFragment()
    {
        // Make the target object disappear
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            Debug.Log($"{gameObject.name} collected and hidden.");
        }
    }
}
