using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    [Header("Assign Fragments in Order of Progression")]
    public List<GameObject> fragments; // List of fragments to assign in the Inspector
    private int currentProgressIndex = 0; // Tracks current progression

    void Start()
    {
        // Ensure all fragments except the first are disabled
        for (int i = 0; i < fragments.Count; i++)
        {
            fragments[i].SetActive(i == currentProgressIndex);
        }
    }

    public void UnlockNextFragment()
    {
        if (currentProgressIndex < fragments.Count - 1)
        {
            // Disable the current fragment
            fragments[currentProgressIndex].SetActive(false);

            // Unlock and enable the next fragment
            currentProgressIndex++;
            fragments[currentProgressIndex].SetActive(true);
            Debug.Log($"Fragment {currentProgressIndex} unlocked!");
        }
        else
        {
            Debug.Log("All fragments unlocked!");
        }
    }

    public bool IsFragmentActive(GameObject fragment)
    {
        // Check if a specific fragment is currently active
        return fragments[currentProgressIndex] == fragment;
    }
}
