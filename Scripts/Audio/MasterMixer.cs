using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterValue(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicValue(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetEffectsValue(float sliderValue)
    {
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }
}
