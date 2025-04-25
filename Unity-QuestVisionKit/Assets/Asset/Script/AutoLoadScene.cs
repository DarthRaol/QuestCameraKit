using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AutoLoadScene : MonoBehaviour
{
    public string sceneToLoad;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Optional: Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
