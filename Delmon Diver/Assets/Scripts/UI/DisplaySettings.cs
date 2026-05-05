using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class DisplaySettings : MonoBehaviour
{

    public TMP_Dropdown ResDropDown;
    public Toggle FullScreenToggle;

    Resolution[] AllResolutions;
    bool isFullScreen;
    int SelectedResolution;

    List<Resolution> SelectedResolutionList = new List<Resolution>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFullScreen = true;
        AllResolutions = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        string newRes;
        foreach (Resolution res in AllResolutions)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                SelectedResolutionList.Add(res);
            }
        }

        ResDropDown.AddOptions(resolutionStringList);
    }


    public void changeResolution()
    {
        SelectedResolution = ResDropDown.value;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, isFullScreen);
    }

    public void ChangeFullScreen()
    {
        isFullScreen = FullScreenToggle.isOn;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, isFullScreen);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
