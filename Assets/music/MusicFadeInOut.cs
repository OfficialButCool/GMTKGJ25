using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicFadeInOut : MonoBehaviour
{
    public float fadeInDuration = 3f;
    public float fadeOutDuration = 2f;

    private AudioSource audioSource;
    private float targetVolume;
    private Coroutine currentFade;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetVolume = audioSource.volume;
        audioSource.volume = 0f;
        audioSource.Play();
        currentFade = StartCoroutine(FadeIn());
    }

    public void TriggerFadeOut()
    {
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeInDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, targetVolume, time / fadeInDuration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeOutDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeOutDuration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}

