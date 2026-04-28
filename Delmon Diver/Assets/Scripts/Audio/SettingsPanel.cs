using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void OnEnable()
    {
        // Load saved values first
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0f);

        // Then add listeners so changes apply to the mixer
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        sfxSlider.onValueChanged.AddListener(UpdateSoundVolume);
    }

    private void OnDisable()
    {
        // Always remove listeners when panel closes to avoid duplicates
        musicSlider.onValueChanged.RemoveListener(UpdateMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(UpdateSoundVolume);

        SaveVolume();
    }

    private void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    private void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    private void SaveVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        PlayerPrefs.Save();
    }

    public void reset()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }
}