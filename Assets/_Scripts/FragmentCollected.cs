using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentCollected : MonoBehaviour
{
    public GameObject targetObject; // The object to toggle visibility

    void Update()
    {
        // Check for screen tap
        if (Input.GetMouseButtonDown(0)) // For touch devices, a tap is registered as mouse button 0
        {
            // Cast a ray to check what was tapped
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the hit object is the ImageTarget or its child
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    // Toggle the target object's visibility
                    if (targetObject != null)
                    {
                        targetObject.SetActive(!targetObject.activeSelf);
                    }
                }
            }
        }
    }
}