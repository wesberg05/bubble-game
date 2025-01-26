using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource bgmSource;  // AudioSource for background music
    public AudioSource sfxSource; // AudioSource for sound effects

    [Header("Audio Clips")]
    public AudioClip defaultBackgroundMusic; // Default background music
    public AudioClip boilSound;             // Example SFX for boiling sound

    private static AudioManager instance;

    private void Awake()
    {
        // Singleton pattern to ensure only one AudioManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (bgmSource && defaultBackgroundMusic)
        {
            PlayBackgroundMusic(defaultBackgroundMusic);
        }
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (bgmSource && musicClip)
        {
            bgmSource.clip = musicClip;
            bgmSource.loop = true;
            bgmSource.Play();
            Debug.Log($"Playing background music: {musicClip.name}");
        }
    }

    public void PlaySoundEffect(AudioClip sfxClip)
    {
        if (sfxSource && sfxClip)
        {
            sfxSource.PlayOneShot(sfxClip);
            Debug.Log($"Playing sound effect: {sfxClip.name}");
        }
    }

    public void PlayBoilSound()
    {
        if (sfxSource && boilSound)
        {
            StartCoroutine(PlayTimedSound(boilSound, 1.0f)); // Play boil sound for 1 second
        }
    }

    private IEnumerator PlayTimedSound(AudioClip clip, float duration)
    {
        if (sfxSource)
        {
            sfxSource.clip = clip;
            sfxSource.Play();

            yield return new WaitForSeconds(duration);

            sfxSource.Stop(); // Stop playing after the duration
            sfxSource.clip = null; // Clear the clip to avoid overlapping issues
            Debug.Log($"Stopped sound effect: {clip.name}");
        }
    }
}
