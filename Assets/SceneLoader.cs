using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1); // Loads the next scene in build order
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Optional: load by name instead
    }
}

