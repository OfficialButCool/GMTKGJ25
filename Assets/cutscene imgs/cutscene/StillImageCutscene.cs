using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StillImageCutscene : MonoBehaviour
{
    [Header("Cutscene Panels (Still Images)")]
    public GameObject[] cutsceneFrames;

    [Header("Key to Advance")]
    public KeyCode advanceKey = KeyCode.Space;

    [Header("Next Scene Settings")]
    public int nextSceneBuildIndex;  // Set this to your scene's build index in Inspector


    [Header("Auto Advance (optional)")]
    public bool autoAdvance = false;

    [Header("Delays per Frame (seconds)")]
    public float[] advanceDelays;

    [Header("Fade Settings")]
    public Image fadeImage;         // Assign your fullscreen black UI Image here
    public float fadeDuration = 1f; // Fade length in seconds
    public string nextSceneName;    // Scene to load after fade (e.g. "endcutscene")

    private int currentIndex = 0;
    private Coroutine autoAdvanceCoroutine;

    void Start()
    {
        ShowFrame(0);

        if (autoAdvance)
        {
            autoAdvanceCoroutine = StartCoroutine(AutoAdvanceCutscene());
        }
    }

    void Update()
    {
        if (!autoAdvance && Input.GetKeyDown(advanceKey))
        {
            AdvanceCutscene();
        }
    }

    IEnumerator AutoAdvanceCutscene()
    {
        while (true)
        {
            float delay = 2f; // default delay

            if (advanceDelays != null && currentIndex < advanceDelays.Length)
                delay = advanceDelays[currentIndex];

            yield return new WaitForSeconds(delay);

            AdvanceCutscene();

            if (currentIndex >= cutsceneFrames.Length)
                yield break;  // stop coroutine after last frame
        }
    }

    void AdvanceCutscene()
    {
        currentIndex++;

        if (currentIndex >= cutsceneFrames.Length)
        {
            EndCutscene();

            if (autoAdvanceCoroutine != null)
                StopCoroutine(autoAdvanceCoroutine);
        }
        else
        {
            ShowFrame(currentIndex);
        }
    }

    void ShowFrame(int index)
    {
        for (int i = 0; i < cutsceneFrames.Length; i++)
        {
            cutsceneFrames[i].SetActive(i == index);
        }
    }

    void EndCutscene()
    {
        Debug.Log("Cutscene Finished");

        // Start fade and scene load coroutine instead of disabling immediately
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("Fade Image not assigned! Loading scene immediately.");
            SceneManager.LoadScene(nextSceneBuildIndex);
            yield break;
        }

        fadeImage.gameObject.SetActive(true);

        Color c = fadeImage.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneBuildIndex);
    }
}
