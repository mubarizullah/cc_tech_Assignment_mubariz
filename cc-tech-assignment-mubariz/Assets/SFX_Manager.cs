using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    [SerializeField] AudioClip failSound;
    public static void PlaySound(AudioClip audioClip)
    {
        GameObject gb = new GameObject();
        AudioSource audioCom = gb.AddComponent<AudioSource>();
        audioCom.clip = audioClip;
        audioCom.Play();
        Destroy(gb,audioClip.length);
    }
}
