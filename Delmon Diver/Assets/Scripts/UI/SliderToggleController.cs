using UnityEngine;
using UnityEngine.UI;

public class SliderToggleController : MonoBehaviour
{
    public Toggle toggle;
    public Slider slider;

    private float _previousValue;

    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        if (isOn) // Toggle is ON → lock slider
        {
            _previousValue = slider.value;
            slider.value = -80f;
            slider.interactable = false;    // blocks interaction
        }
        else // Toggle is OFF → restore slider
        {
            slider.interactable = true;
            slider.value = _previousValue;
        }
    }
}