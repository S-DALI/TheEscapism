using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MeleeEnemy : MonoBehaviour
    {
        [SerializeField] protected float attaclCoolDawn;
        [SerializeField] protected int damage;
        [SerializeField] protected float range;
        [SerializeField] protected float colliderDistance;
        [SerializeField] protected BoxCollider2D BoxCollider;
        [SerializeField] protected LayerMask player;
        [SerializeField] protected Hero PlayerHP;
        [SerializeField] protected AudioSource Attack;
        protected float attackTimer = Mathf.Infinity;
        protected Animator Anim;


        protected void Awake()
        {
            Anim = GetComponent<Animator>();
        }
        virtual protected void Update()
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


        protected bool PlayerinSign()
        {
            RaycastHit2D hit = Physics2D.BoxCast(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.y), 0, Vector2.left, 0, player);
            return hit.collider != null;
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.y));
        }
        protected void DamagePlayer()
        {
            if (PlayerinSign())
            {
                Hero.Instance.GetDamage(damage);
            }
            
                
        }
        protected void AudioPlay()
        {
            Attack.Play();
        }

        protected void AudioStop()
        {
            Attack.Stop();
        }
    }
}
