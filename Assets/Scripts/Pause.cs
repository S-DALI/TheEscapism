using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject panel;
    public void LevelPause()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void LevelContinue()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel(int NumberThisScene)
    {
        panel.SetActive(false);
        SceneManager.LoadScene(NumberThisScene);
        Time.timeScale = 1;
    }
}
