using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager :
MonoBehaviour
{
    public static AudioManager
        Instance;

    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    AudioSource sfxSource;
    [SerializeField]
    AudioClip menuMusic;
    [SerializeField]
    AudioClip gameMusic;

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
    }

    void Start()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        RefreshVolumes();
    }

    public void RefreshVolumes()
    {
        if (musicSource != null)
        {
            musicSource.volume =
                GameSettingsManager
                .Instance
                .MusicVolume;
        }

        if (sfxSource != null)
        {
            sfxSource.volume =
                GameSettingsManager
                .Instance
                .SfxVolume;
        }
    }

    public void PlaySfx(
        AudioClip clip)
    {
        if (clip == null)
            return;

        sfxSource.PlayOneShot(
            clip,
            GameSettingsManager
                .Instance
                .SfxVolume);
    }

    public void PlayMusic(
        AudioClip clip)
    {
        if (clip == null)
            return;

        if (musicSource.clip == clip)
            return;

        musicSource.clip =
            clip;

        musicSource.Play();
    }
    void OnSceneLoaded(
    Scene scene,
    LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            PlayMusic(menuMusic);
        }
        else
        {
            PlayMusic(gameMusic);
        }
    }
}