using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class LevelsMenu : MonoBehaviour
    {
        public Button[] levels;
        public int LevelsCounter = 1;
        void Start()
        {
            PlayerPrefs.DeleteAll();
            int LevelsReached = PlayerPrefs.GetInt("LevelReached", LevelsCounter);
            for (int i = 0; i < levels.Length; i++)
            {
                if (i + 1 > LevelsReached)
                {
                    levels[i].interactable = false;
                }
            }
        }

        public void Selected(int NumberLevel)
        {
            SceneManager.LoadScene(NumberLevel);
        }
    }
}