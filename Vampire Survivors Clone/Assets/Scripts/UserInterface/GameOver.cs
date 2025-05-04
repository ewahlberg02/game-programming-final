using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool isDead = false;
    public GameObject GameOverScreen;

   
    public void endGame()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        isDead = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
