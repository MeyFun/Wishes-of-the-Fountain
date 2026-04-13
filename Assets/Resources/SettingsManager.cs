using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        float mVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        musicSlider.value = mVol;
        sfxSlider.value = sVol;

        SetMusicVolume(mVol);
        SetSFXVolume(sVol);
    }

    public void SetMusicVolume(float value)
    {
        // Математика децибел: логарифмическое затухание
        float dB = Mathf.Log10(value) * 20;
        mainMixer.SetFloat("MusicVol", dB);

        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        float dB = Mathf.Log10(value) * 20;
        mainMixer.SetFloat("SFXVol", dB);

        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}