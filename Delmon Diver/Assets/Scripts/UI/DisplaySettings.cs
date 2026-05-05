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

    void Start()
    {
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

        // Load saved values after populating dropdown
        LoadSettings();
    }

    public void changeResolution()
    {
        SelectedResolution = ResDropDown.value;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, isFullScreen);
        SaveSettings();
    }

public void ChangeFullScreen()
{
    // Guard against being called before list is populated
    if (SelectedResolutionList == null || SelectedResolutionList.Count == 0) return;

    isFullScreen = FullScreenToggle.isOn;
    Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, isFullScreen);
    SaveSettings();
}

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("Resolution", SelectedResolution);
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        // Load fullscreen
        isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        FullScreenToggle.isOn = isFullScreen;

        // Load resolution
        SelectedResolution = PlayerPrefs.GetInt("Resolution", SelectedResolutionList.Count - 1); // default to highest
        ResDropDown.value = SelectedResolution;
        ResDropDown.RefreshShownValue();

        // Apply loaded settings
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, isFullScreen);
    }
}