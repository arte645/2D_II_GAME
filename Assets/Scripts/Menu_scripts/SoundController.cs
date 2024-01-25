using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip click;
    public AudioClip error;

    public void ClickSound()
    {
        sound.PlayOneShot(click);
    }
    public void ErrorSound()
    {
        sound.PlayOneShot(error);
    }
}
