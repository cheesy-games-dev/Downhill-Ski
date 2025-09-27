using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }
    public AudioSource AudioSource;
    void Start()
    {
        Instance = this;
        AudioSource = GetComponent<AudioSource>();
        AudioSource.Play();
    }
    public void TogglePause(bool pause)
    {
        if (pause)
            AudioSource.Pause();
        else
            AudioSource.UnPause();
    }

    public void ChangeSongs()
    {
        AudioSource.Play();
    }
}
