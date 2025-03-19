using UnityEngine;
using UnityEngine.Audio;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem Instance;

    [Header("Audio Mixer")] [SerializeField]
    private AudioMixer audioMixer;

    [Header("Volume Settings")] [Range(0.0001f, 1f)] [SerializeField]
    private float masterVolume = 1f;

    [Range(0.0001f, 1f)] [SerializeField] private float sfxVolume = 1f;

    [Range(0.0001f, 1f)] [SerializeField] private float backgroundMusicVolume = 1f;

    public float MasterVolume
    {
        get => masterVolume;
        private set => masterVolume = value;
    }

    public float SfxVolume
    {
        get => sfxVolume;
        private set => sfxVolume = value;
    }

    public float BackgroundMusicVolume
    {
        get => backgroundMusicVolume;
        private set => backgroundMusicVolume = value;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master")) LoadVolume();
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        audioMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("Master", masterVolume);
    }

    public void SetBackgroundMusicVolume(float value)
    {
        backgroundMusicVolume = value;
        audioMixer.SetFloat("BackgroundMusic", Mathf.Log10(backgroundMusicVolume) * 20);
        PlayerPrefs.SetFloat("BackgroundMusic", backgroundMusicVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("SFX", sfxVolume);
    }

    private void LoadVolume()
    {
        masterVolume = PlayerPrefs.GetFloat("Master");
        backgroundMusicVolume = PlayerPrefs.GetFloat("BackgroundMusic");
        sfxVolume = PlayerPrefs.GetFloat("SFX");

        audioMixer.SetFloat("Master",
            Mathf.Log10(masterVolume) * 20);
        audioMixer.SetFloat("BackgroundMusic",
            Mathf.Log10(backgroundMusicVolume) * 20);
        audioMixer.SetFloat("SFX",
            Mathf.Log10(sfxVolume) * 20);
    }
}