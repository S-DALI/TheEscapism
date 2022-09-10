using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets {
    public class PortalScriptLevelsComplete : MonoBehaviour
    {
        [SerializeField] private int EnemyinLevel;
      
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == Hero.Instance.gameObject)
            {
                UnlockLevel();
                SceneManager.LoadScene(1);
            }
        }

        private void UnlockLevel()
        {
            int CurrentLevel = SceneManager.GetActiveScene().buildIndex;
            if(CurrentLevel > PlayerPrefs.GetInt("LevelReached"))
            {
                PlayerPrefs.SetInt("LevelReached", CurrentLevel++);
            }
        }
    }
} 