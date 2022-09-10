using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float Speed;
        [SerializeField] private float LifeTime;
        [SerializeField] private float Distance;
        [SerializeField] private int Damage;
        [SerializeField] private LayerMask Solid;
        [HideInInspector] public Vector2 Direction;

        private void Start()
        {
            Destroy(gameObject, LifeTime);
        }
        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, Distance, Solid);
            if(hit.collider !=null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponent<Hero>().GetDamage(Damage);
                }
                Destroy(gameObject);
            }
            transform.Translate(Direction * Speed * Time.deltaTime);
            
        }
    }
}

