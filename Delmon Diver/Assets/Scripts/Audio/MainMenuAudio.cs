using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
 
public class MainMenuAudio : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
 
    private void Start()
    {
        LoadVolume();
        MusicManager.Instance.PlayMusic("A");
    }
    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
}