using UnityEngine;
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
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void Save()
    {
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

        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1);
        globalVolumeSlider.onValueChanged.AddListener(SetVolume);
        musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 1);
        musicVolumeSlider.value = musicSource.volume;
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        globalVolumeSlider.value = AudioListener.volume;
    }
}
