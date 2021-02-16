using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
        Debug.Log("exit");
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<ScenePersist>().DestroyScenePersist();
        LoadNextScene();
    }

    
}
