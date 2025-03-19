using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider sfxSlider;

    private SoundSystem soundSystem;

    private void OnEnable()
    {
        if (soundSystem == null) soundSystem = SoundSystem.Instance;

        masterSlider.value = soundSystem.MasterVolume;
        backgroundSlider.value = soundSystem.BackgroundMusicVolume;
        sfxSlider.value = soundSystem.SfxVolume;
    }

    public void SetMasterVolume()
    {
        Debug.Log(masterSlider.value);
        soundSystem.SetMasterVolume(masterSlider.value);
    }

    public void SetBackgroundMusicVolume()
    {
        soundSystem.SetBackgroundMusicVolume(backgroundSlider.value);
    }

    public void SetSFXVolume()
    {
        soundSystem.SetSFXVolume(sfxSlider.value);
    }
}