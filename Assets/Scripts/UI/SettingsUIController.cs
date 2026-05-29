using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController :
MonoBehaviour
{
    [SerializeField]
    Slider mouseSlider;

    [SerializeField]
    Slider musicSlider;

    [SerializeField]
    Slider sfxSlider;

    void OnEnable()
    {
        mouseSlider.value =
            GameSettingsManager
            .Instance
            .MouseSensitivity;

        musicSlider.value =
            GameSettingsManager
            .Instance
            .MusicVolume;

        sfxSlider.value =
            GameSettingsManager
            .Instance
            .SfxVolume;
    }

    public void OnMouseChanged(
        float value)
    {
        GameSettingsManager
            .Instance
            .SetMouseSensitivity(
                value);
    }

    public void OnMusicChanged(
        float value)
    {
        GameSettingsManager
            .Instance
            .SetMusicVolume(
                value);
    }

    public void OnSfxChanged(
        float value)
    {
        GameSettingsManager
            .Instance
            .SetSfxVolume(
                value);
    }

    public void Back()
    {
        MainUIManager
            .Instance
            .HideSettings();
    }
}