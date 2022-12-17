using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class WallBoss : MonoBehaviour
    {
        private Rigidbody2D Wall;
        [SerializeField]private MonstersHealth HP_Boss;
        void Start()
        {
            Wall = GetComponent<Rigidbody2D>();
        }
        protected virtual void Update()
        {
            if(HP_Boss.currentHealth<=0)
            {
                Destroy(this.gameObject);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Equals("Hero"))
                Wall.isKinematic = false;
        }
       

    }
}
