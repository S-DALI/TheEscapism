using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    
    public class Saw : MonoBehaviour
    {
        [SerializeField] AudioSource saw;
        [SerializeField] Transform playerposition;
        [SerializeField] private float distancesound;

        private void Update()
        {
            transform.Rotate(0.0f, 0.0f, 1f);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == Hero.Instance.gameObject)
            {
                
                Hero.Instance.GetDamageFromTrap();
                
            }
        }
    }
}
