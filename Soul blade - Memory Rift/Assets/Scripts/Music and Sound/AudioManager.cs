using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    private Coroutine musicRoutine;

    private void Awake()
    {
        ServiceLocator.Register(this);
    }

    public void PlayMusic(AudioClip clip, float volume = 1, float fadeTime = 0.5f)
    {
        if(musicSource.clip == clip)
        {
            return;
        }

        if (musicRoutine != null)
        {
            StopCoroutine(musicRoutine);
        }
        musicRoutine = StartCoroutine(PlayMusicRoutine(clip,volume,fadeTime));
    }

    IEnumerator PlayMusicRoutine(AudioClip clip, float volume, float fadeTime)
    {
        yield return Fade(0, fadeTime);
        
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();

        yield break;
    }

    IEnumerator Fade(float target, float duration)
    {
        float start = musicSource.volume;
        float time = 0;

        while(time < duration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(start, target, time/duration);
            yield return null;
        }
    }
}