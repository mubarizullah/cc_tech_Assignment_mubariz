using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    bool musicOn = true;
    AudioSource musicSource;
    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void MusicOnOrOff()
    {
        if (musicSource != null)
        {
            if(musicOn)
            {
                musicSource.Pause();
                musicOn = false;
            }
            else
            {
                musicSource.Play();
                musicOn = true;
            }
        }
    }
}
