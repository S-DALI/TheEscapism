using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("LevelReached")-1);
    }
    public void Selected(int NumberScene)
    {
        SceneManager.LoadScene(NumberScene);
    }
}
