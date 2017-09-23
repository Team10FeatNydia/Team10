using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour {
    public Slider bgmSlider;
    public Slider sfxSlider;

    // Use this for initialization
    void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGM_Slider");
        SoundManagerScript.Instance.bgmAudioSource.volume = bgmSlider.value;

        sfxSlider.value = PlayerPrefs.GetFloat("SFX_Slider");
        SoundManagerScript.Instance.sfxAudioSource.volume = sfxSlider.value;
    }

    public void BGMChange()
    {
        PlayerPrefs.SetFloat("BGM_Slider", bgmSlider.value);
    }

    public void SFXChange()
    {
        PlayerPrefs.SetFloat("SFX_Slider", sfxSlider.value);
    }
}
