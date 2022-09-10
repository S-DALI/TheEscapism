using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Assets
{
    public class PortalToThisLevel : MonoBehaviour
    {
        [SerializeField] private int EnemyinLevel;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == Hero.Instance.gameObject)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+0);
            }
        }
    }
}

