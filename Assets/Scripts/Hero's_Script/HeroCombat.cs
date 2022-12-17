using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class HeroCombat : MonoBehaviour
    {
        [SerializeField] private AudioSource AttackSound;
        public Transform AttackPoint;
        public LayerMask enemyLayers;
        private SpriteRenderer sprite;
        public float AttackRange = 0.5f;
        

        public void TakeDamage(int Damage)
        {
            AttackSound.Play();
            Collider2D[] EnemyColliders = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);
            if (EnemyColliders != null)
            {
                foreach (Collider2D enemy in EnemyColliders)
                {
                    enemy.GetComponent<MonstersHealth>().GetDamage(Damage);
                }
            }
        }
        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
    }
}
