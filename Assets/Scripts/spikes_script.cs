using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class spikes_script : MonoBehaviour
    {


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == Hero.Instance.gameObject)
            {
                
                Hero.Instance.GetDamageFromTrap();
            }
        }
    }
}
