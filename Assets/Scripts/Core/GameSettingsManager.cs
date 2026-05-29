using UnityEngine;

public class GameSettingsManager :
MonoBehaviour
{
    public static GameSettingsManager
        Instance;

    const string MouseKey =
        "MouseSensitivity";

    const string MusicKey =
        "MusicVolume";

    const string SfxKey =
        "SfxVolume";

    public float MouseSensitivity
    {
        get;
        private set;
    }

    public float MusicVolume
    {
        get;
        private set;
    }

    public float SfxVolume
    {
        get;
        private set;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(
            gameObject);

        LoadSettings();
    }

    void LoadSettings()
    {
        MouseSensitivity =
            PlayerPrefs.GetFloat(
                MouseKey,
                2f);

        MusicVolume =
            PlayerPrefs.GetFloat(
                MusicKey,
                1f);

        SfxVolume =
            PlayerPrefs.GetFloat(
                SfxKey,
                1f);
    }

    public void SetMouseSensitivity(
        float value)
    {
        MouseSensitivity =
            value;

        SaveFloat(
            MouseKey,
            value);
    }

    public void SetMusicVolume(
        float value)
    {
        MusicVolume =
            value;

        SaveFloat(
            MusicKey,
            value);

        if (AudioManager.Instance != null)
        {
            AudioManager
                .Instance
                .RefreshVolumes();
        }
    }

    public void SetSfxVolume(
        float value)
    {
        SfxVolume =
            value;

        SaveFloat(
            SfxKey,
            value);

        if (AudioManager.Instance != null)
        {
            AudioManager
                .Instance
                .RefreshVolumes();
        }
    }

    void SaveFloat(
        string key,
        float value)
    {
        PlayerPrefs.SetFloat(
            key,
            value);

        PlayerPrefs.Save();
    }
}