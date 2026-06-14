using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager :
MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] List<AudioClip> musics = new List<AudioClip>();
    [SerializeField] List<SoundData> sounds = new List<SoundData>(); 

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
        MusicForScene("Main");
    }

    void Start()
    {
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
    public void PlaySfx(int id)
    {
        if (id < 0 || id >= sounds.Count) return;
        SoundData sound = sounds[id];
        float oldPitch = sfxSource.pitch;
        sfxSource.pitch = sound.pitch;
        sfxSource.PlayOneShot(sound.clip, sound.volume * GameSettingsManager.Instance.SfxVolume);
        sfxSource.pitch = oldPitch;
    }

    public void PlayMusic( AudioClip clip){
        if (clip == null || musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void MusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Main":
                PlayMusic(musics[0]);
                break;
            case "CoreGame":
                PlayMusic(musics[1]);
                break;
            case "ForestCamp":
                PlayMusic(musics[2]);
                break;
            case "AbandonedVillage":
                PlayMusic(musics[3]);
                break;
            case "Desert":
                PlayMusic(musics[4]);
                break;
            case "LostCivilization":
                PlayMusic(musics[5]);
                break;

        }
    }
}