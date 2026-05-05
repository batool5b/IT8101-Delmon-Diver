using UnityEngine;
using UnityEngine.UI;

public class ToggleVisibilityController : MonoBehaviour
{
    public Toggle toggleA;
    public Toggle toggleB;

    public GameObject elementA; // shown when toggleA is on
    public GameObject elementB; // shown when toggleB is on

    private void OnEnable()
    {
        toggleA.onValueChanged.AddListener(OnToggleAChanged);
        toggleB.onValueChanged.AddListener(OnToggleBChanged);

        // Set initial state
        toggleA.isOn = true;
        toggleB.isOn = false;
        elementA.SetActive(true);
        elementB.SetActive(false);
    }

    private void OnDisable()
    {
        toggleA.onValueChanged.RemoveListener(OnToggleAChanged);
        toggleB.onValueChanged.RemoveListener(OnToggleBChanged);
    }

    private void OnToggleAChanged(bool isOn)
    {
        elementA.SetActive(isOn);

        if (isOn)
        {
            toggleB.isOn = false; // turn off the other toggle
            elementB.SetActive(false);
        }
    }

    private void OnToggleBChanged(bool isOn)
    {
        elementB.SetActive(isOn);

        if (isOn)
        {
            toggleA.isOn = false; // turn off the other toggle
            elementA.SetActive(false);
        }
    }
}