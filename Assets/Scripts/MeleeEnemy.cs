using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MeleeEnemy : MonoBehaviour
    {
        [SerializeField] private float attaclCoolDawn;
        [SerializeField] private int damage;
        [SerializeField] private float range;
        [SerializeField] private float colliderDistance;
        [SerializeField] private BoxCollider2D BoxCollider;
        [SerializeField] private LayerMask player;
        [SerializeField] private Hero PlayerHP;
        [SerializeField] private AudioSource Attack;
        private float attackTimer = Mathf.Infinity;
        private Animator Anim;


        private void Awake()
        {
            Anim = GetComponent<Animator>();
        }
        private void Update()
        {
            attackTimer += Time.deltaTime;

            if (PlayerinSign() && PlayerHP.HP > 0)
            {
                if (attackTimer >= attaclCoolDawn)
                {
                   
                    attackTimer = 0;
                    Anim.SetTrigger("Attack");
                }
            }
        }


        private bool PlayerinSign()
        {
            RaycastHit2D hit = Physics2D.BoxCast(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.y), 0, Vector2.left, 0, player);
            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.y));
        }
        public void DamagePlayer()
        {
            if (PlayerinSign())
            {
                Hero.Instance.GetDamage(damage);
            }
            
                
        }
        private void AudioPlay()
        {
            Attack.Play();
        }

        private void AudioStop()
        {
            Attack.Stop();
        }
    }
}
