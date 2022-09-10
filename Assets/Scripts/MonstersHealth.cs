using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MonstersHealth : MonoBehaviour
    {
        [SerializeField] public Animator Monster;
        public int currentHealth = 100;
        [Header("Audio")]
        [SerializeField] private AudioSource Hurt;
        
        public void GetDamage(int damage)
        {
            Monster.SetTrigger("Get_Damage");
            Hurt.Play();
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
            Die();
            }
            

        }
        public void Die()
        {
            Hero.Instance.CurrentKill++;
            this.enabled = false;
            Monster.SetBool("Bandit_leave", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Collider2D>().enabled = false;

        }
        public void Destroy()
        {
            Destroy(this.gameObject);
        }
      
      
    }
}
