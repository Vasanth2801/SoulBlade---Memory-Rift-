using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioData musicClip;
    [SerializeField] private float volume;

    private bool hasPlayed;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasPlayed)
        {
            return;
        }

        if (!collision.CompareTag("Player"))
        {
            return;
        }

        AudioManager audio = ServiceLocator.Get<AudioManager>();

        audio.PlayMusic(musicClip, volume);

        hasPlayed = true;
    }
}
