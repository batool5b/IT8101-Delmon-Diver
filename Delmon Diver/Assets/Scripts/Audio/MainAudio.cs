using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
 
public class MainAudio : MonoBehaviour
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
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0f); // 0f = default
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0f);
    }
}
 