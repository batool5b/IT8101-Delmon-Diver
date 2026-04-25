using UnityEngine;
using UnityEngine.UI;

public class Level1SequenceManager : MonoBehaviour
{
    [Header("Objectives Tracker")]
    public bool hasAxe = false;
    public bool cabinDoorBroken = false;
    public bool hasBackpack = false;
    
    // [Header("UI References (Optional)")]
    // E.g., a simple text element to update the player's current goal
    // public Text objectiveText;

    private void Start()
    {
        UpdateObjectiveUI("Escape the sinking ship. The door is jammed.");
    }

    public void CollectAxe()
    {
        hasAxe = true;
        UpdateObjectiveUI("Axe acquired. Break down the cabin door.");
    }

    public void BreakCabinDoor()
    {
        if (hasAxe)
        {
            cabinDoorBroken = true;
            UpdateObjectiveUI("Climb the sail rigging to reach upper deck.");
            // Here you can disable the door's collider or play a breaking animation
            Debug.Log("Door Broken!");
        }
        else
        {
            Debug.Log("You need an axe to break this door.");
        }
    }

    public void CollectBackpack()
    {
        hasBackpack = true;
        UpdateObjectiveUI("Backpack collected. Run to the lifeboat station!");
    }

    public void ReachLifeboat()
    {
        if (hasBackpack)
        {
            UpdateObjectiveUI("Escaped! Level 1 Complete.");
            Debug.Log("Level Complete Sequence Start");
            // Load next scene or show score screen here
        }
        else
        {
            UpdateObjectiveUI("You must get your backpack from storage first!");
            Debug.Log("Need backpack.");
        }
    }

    private void UpdateObjectiveUI(string newText)
    {
        // if (objectiveText != null)
        // {
        //     objectiveText.text = "Objective: " + newText;
        // }
        Debug.Log("Objective Update: " + newText);
    }
}
