using UnityEngine;
using UnityEngine.UI;

public class SavedParameter : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetFloat("BestScore", 0);
            PlayerPrefs.SetFloat("Music", .5f);
            PlayerPrefs.SetFloat("ScreenShake", 1);
            PlayerPrefs.SetFloat("AngledCamera", 1);
        }
    }

    public void SetMusicValue(float value)
    {
        PlayerPrefs.SetFloat("Music", value);
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
