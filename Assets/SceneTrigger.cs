using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeTrigger : MonoBehaviour
{
    [Header("endcutscene")]
    public string endcutscene = "endcutscene";

    [Header("Fade UI CanvasGroup")]
    public CanvasGroup fadeCanvasGroup;

    [Header("Fade Settings")]
    public float fadeDuration = 1f;

    private bool isFading = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFading && other.CompareTag("Player"))
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        isFading = true;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(endcutscene);
    }
}


