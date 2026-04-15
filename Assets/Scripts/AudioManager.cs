using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume; // 0 = mute, 1 = full
    }

    public void Mute(bool mute)
    {
        audioSource.mute = mute;
    }
}