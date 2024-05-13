using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public int levelToLoad;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void OnFadeCompele()
    {
        SceneManager.LoadScene(levelToLoad);
        GordonScript.GameOver = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

