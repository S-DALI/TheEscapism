using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets {
    public class High_trap : MonoBehaviour
    {
        private Rigidbody2D rb_trap;
        void Start()
        {
            rb_trap = GetComponent<Rigidbody2D>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Equals("Hero"))
                rb_trap.isKinematic = false;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == Hero.Instance.gameObject)
                Hero.Instance.GetDamageFromTrap();
        }
        
    }
}
