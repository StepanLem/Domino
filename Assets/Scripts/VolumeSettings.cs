using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private UIElement self;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider globalVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Button cancel;
    [SerializeField] private Button save;
    [SerializeField] private CancelEvent cancelEvent;
    [SerializeField] private Toggle toggle;
    private bool postProcess
    {
        get
        {
            return Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing;
        }
        set
        {
            Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing = value;
        }
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void SetPostProcessing(bool value)
    {
        postProcess = value;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
        PlayerPrefs.SetFloat("musicVolume", musicSource.volume);
        PlayerPrefs.SetInt("postProcessing", Convert.ToInt32(postProcess));
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        Syncronize();
        cancel.onClick.AddListener(Syncronize);
        save.onClick.AddListener(Save);
        cancel.onClick.AddListener(self.Hide);
        save.onClick.AddListener(self.Hide);
        self.OnShow.AddListener(Syncronize);
        cancelEvent.OnCancel.AddListener(Syncronize);
        cancelEvent.OnCancel.AddListener(self.Hide);
    }

    public void Syncronize()
    {
        globalVolumeSlider.onValueChanged.RemoveAllListeners();
        musicVolumeSlider.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.RemoveAllListeners();

        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1);
        globalVolumeSlider.onValueChanged.AddListener(SetVolume);
        musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 1);
        musicVolumeSlider.value = musicSource.volume;
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        globalVolumeSlider.value = AudioListener.volume;

        postProcess = Convert.ToBoolean(PlayerPrefs.GetInt("postProcessing", 1));
        toggle.isOn = postProcess;
        toggle.onValueChanged.AddListener(SetPostProcessing);
    }
}
