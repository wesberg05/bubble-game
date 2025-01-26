using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource;  // AudioSource for background music
    public AudioSource characterNoisesSource; // AudioSource for character noises

    [Header("UI Elements")]
    public Slider musicSlider;   // Slider for background music volume
    public Slider characterSlider; // Slider for character noises volume

    private void Start()
    {
        // Set both sliders to 1 by default (maximum volume)
        musicSlider.value = 0.5f;
        characterSlider.value = 0.8f;

        // Apply the volume levels to audio sources
        backgroundMusicSource.volume = musicSlider.value;
        characterNoisesSource.volume = characterSlider.value;

        // Add listeners to sliders to adjust volume when changed
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        characterSlider.onValueChanged.AddListener(OnCharacterVolumeChanged);
    }

    private void OnMusicVolumeChanged(float value)
    {
        // Update background music volume
        backgroundMusicSource.volume = value;
    }

    private void OnCharacterVolumeChanged(float value)
    {
        // Update character noises volume
        characterNoisesSource.volume = value;
    }
}
