using UnityEngine;

public class GPAudioScript : MonoBehaviour
{

    public AudioTypes audioTypes;

    public enum Audios { BOOM,LASER};

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Play sound
    public void PlaySound(Audios audio)
    {
        AudioClip clip = null;
        switch (audio)
        {
            case Audios.BOOM:
                { clip = audioTypes.boomSound; }
                break;
            case Audios.LASER:
                { clip = audioTypes.laserSound; }
                break;
            default:
                break;
        }
        audioSource.PlayOneShot(clip);
    }
}
