using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip boilSound;

    private void Start()
    {
        // Setup background music
        if (bgmSource && backgroundMusic)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlayBoilSound()
    {
        if (sfxSource && boilSound)
        {
            sfxSource.PlayOneShot(boilSound);
        }
    }
}
