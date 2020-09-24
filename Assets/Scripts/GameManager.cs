using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LaunchMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        PlayerPrefs.SetFloat("Lives", 3f);
        SceneManager.LoadScene("Tutorial");
    }

    public void NextStage()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "Tutorial")
        {
            SceneManager.LoadScene("1");
        }
        else
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public void RestartLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void Die()
    {
        float lives = PlayerPrefs.GetFloat("Lives");
        if (lives > 1)
        {
            PlayerPrefs.SetFloat("Lives", lives-1);
            RestartLevel();
        }
        else
        {
            SceneManager.LoadScene("Died");
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndGame");
    }
}
