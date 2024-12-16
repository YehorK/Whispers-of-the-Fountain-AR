using UnityEngine;

public class ActivateOgopogo : MonoBehaviour
{
    [SerializeField] private GameObject targetParentObject; // The parent object whose children will be monitored
    [SerializeField] private GameObject objectToActivate;  // The object to activate when children are destroyed
    [SerializeField] private GameObject objectToHide;  // The object to activate when children are destroyed

    void Update()
    {
        // Check if the target parent object has no children left
        if (targetParentObject != null && targetParentObject.transform.childCount == 0)
        {
            // Activate the specified object
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                objectToHide.SetActive(false);
            }

            // Optional: Disable this script after activation to prevent unnecessary checks
            this.enabled = false;
        }
    }
}