using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class SamuraiCombat : MonoBehaviour
    {
        public Animator animator;
        public Transform AttackPoint;
        public LayerMask enemyLayers;
        private SpriteRenderer sprite;
        public int AttackDamage = 20;
        public float AttackRange = 0.5f;

      
        public void Attack()
        {

            animator.SetTrigger("Attack");
            Collider2D[] EnemyColliders = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);
            foreach (Collider2D enemy in EnemyColliders)
            {
                enemy.GetComponent<MonstersHealth>().GetDamage(AttackDamage);
            }

        }
        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
    }
}
