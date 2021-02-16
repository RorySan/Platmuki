using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int coinScore;
    [SerializeField] int totalCoinScore;



    private void Awake()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerDeath()
    {
        playerLives--;
        if (playerLives<= 0)
        {
            Debug.Log("HAS PALMAO DEL TÓ");
        }
        else
        {
            StartCoroutine(ReloadLevel());
        }
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(3f);
        totalCoinScore -= coinScore;
        coinScore = 0;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void CoinPickup()
    {
        coinScore++;
        totalCoinScore++;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


}
