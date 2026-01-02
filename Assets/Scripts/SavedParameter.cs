using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SavedParameter : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetFloat("BestScore", 0);
            PlayerPrefs.SetFloat("Music", .5f);
            PlayerPrefs.SetFloat("ScreenShake", 1);
            PlayerPrefs.SetFloat("AngledCamera", 1);
        }
        SetMixerValue();
    }

    public void SetMusicValue(float value)
    {
        PlayerPrefs.SetFloat("Music", value);
        SetMixerValue();
    }

    private void SetMixerValue()
    {
        float value = PlayerPrefs.GetFloat("Music");
        float volume = 20 * Mathf.Log10(value);
        if (value == 0)
            volume = -144f;
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetScreenShakeValue(float value)
    {
        PlayerPrefs.SetFloat("ScreenShake", value);
        CameraShake.Instance.CheckForceScreenShake();
    }

    public void SetAngledCameraValue(float value)
    {
        PlayerPrefs.SetFloat("AngledCamera", value);
        CameraShake.Instance.CheckValueAngledCamera();
    }
}
