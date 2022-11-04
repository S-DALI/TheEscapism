using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class FireRainScript : MonoBehaviour
    {
        [SerializeField] protected int damage;
        [SerializeField] protected float range;
        [SerializeField] protected float colliderDistance;
        [SerializeField] protected BoxCollider2D BoxCollider;
        [SerializeField] protected LayerMask player;
        [SerializeField] protected Hero PlayerHP;
        [SerializeField] protected AudioSource Attack;
        protected float attackTimer = Mathf.Infinity;
        protected Animator FireRain;


        protected void Awake()
        {
            FireRain = GetComponent<Animator>();

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Equals("Hero") || collision.gameObject.layer.Equals(7))
            {
                FireRain.SetTrigger("Boom");
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
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
        public void DestroyObject()
        {
            Destroy(this.gameObject);
        }
    }
}
