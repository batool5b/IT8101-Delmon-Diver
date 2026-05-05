using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicToggle;
    public Toggle sfxToggle;

private void OnEnable()
{
    // Load saved values
    musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
    sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0f);

    // Load saved toggle states
    musicToggle.isOn = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
    sfxToggle.isOn = PlayerPrefs.GetInt("SFXMuted", 0) == 1;

    // Add listeners FIRST so mixer updates when invoke fires
    musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
    sfxSlider.onValueChanged.AddListener(UpdateSoundVolume);

    // Only invoke if muted, otherwise just apply mixer value directly
    if (musicToggle.isOn)
        musicToggle.onValueChanged.Invoke(true);
    else
        audioMixer.SetFloat("MusicVolume", musicSlider.value);

    if (sfxToggle.isOn)
        sfxToggle.onValueChanged.Invoke(true);
    else
        audioMixer.SetFloat("SFXVolume", sfxSlider.value);
}

    private void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(UpdateMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(UpdateSoundVolume);

        SaveVolume();
    }

    private void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        SaveVolume(); // save on every change
    }

    private void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        SaveVolume(); // save on every change
    }

public void SaveVolume()
{
    // Save slider values directly instead of reading from mixer
    PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);

    PlayerPrefs.SetInt("MusicMuted", musicToggle.isOn ? 1 : 0);
    PlayerPrefs.SetInt("SFXMuted", sfxToggle.isOn ? 1 : 0);

    PlayerPrefs.Save();
}

    public void reset()
    {
        musicSlider.value = 0f;
        sfxSlider.value = 0f;
        musicToggle.isOn = false;
        sfxToggle.isOn = false;
    }
}