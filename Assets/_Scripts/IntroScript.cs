using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public GameObject targetObject; // The object to toggle visibility
    public KeyCode testKey = KeyCode.Space; // Key for testing on PC
    private GameProgressManager progressManager;

    void Start()
    {
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
            Debug.Log("Testing fragment collection via space key.");
            if (progressManager != null && progressManager.IsFragmentActive(gameObject))
            {
                Debug.Log($"{gameObject.name} collected via test key!");
                CollectFragment();
                progressManager.UnlockNextFragment();
            }
            else
            {
                Debug.Log("This fragment is not active yet.");
            }
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
                if (progressManager != null && progressManager.IsFragmentActive(gameObject))
                {
                    Debug.Log($"{gameObject.name} collected!");
                    CollectFragment();
                    progressManager.UnlockNextFragment();
                }
                else
                {
                    Debug.Log($"{gameObject.name} is not active yet.");
                }
            }
        }
    }

    private void CollectFragment()
    {
        // Hide or deactivate the target object
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
