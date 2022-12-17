using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets {
    public class Shoot_Enemy : MonoBehaviour
    {
        [SerializeField] private Transform[] SpawnFire;
        private float TimerShoots;
        private bool FacingRight = true;
        [SerializeField] private float StartShoots;
        [SerializeField] private GameObject FireBall;
        [SerializeField] private MonstersHealth HP_Enemy;
        [SerializeField] private Animator Enemy_Animator;
        [SerializeField] private Transform Hero;
        [SerializeField] private float Offset;
        [SerializeField] private GameObject Rotate;
        [SerializeField] private float AgroDistance;

        void Start()
        {
            TimerShoots = 0f;
        }

        void Update()
        {
            float DistanceToPlayer = Vector2.Distance(transform.position, Hero.transform.position);
            if (DistanceToPlayer <= AgroDistance)
            {
                TimerShoots += Time.deltaTime;
                if (HP_Enemy.currentHealth > 0 && TimerShoots >= StartShoots)
                {
                    Enemy_Animator.SetTrigger("Start_Shoot");
                }

                if (Hero.position.x < transform.position.x && transform.localScale.x > 0 || Hero.position.x > transform.position.x && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    Offset = 0f;
                    FireBall.GetComponent<FireBall>().Direction = Vector2.right;
                }
                else
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    Offset = 180f;
                    FireBall.GetComponent<FireBall>().Direction = Vector2.left;
                }
                //if (!FacingRight && Hero.transform.position.x > transform.position.x)
                //{
                //    Flip();
                //    Offset = 0f;
                //    FireBall.GetComponent<FireBall>().Direction = Vector2.right;
                //}
                //else if (FacingRight && Hero.transform.position.x < transform.position.x)
                //{
                //    Flip();
                //    Offset = 180f;
                //    FireBall.GetComponent<FireBall>().Direction = Vector2.left;
                //}
                Vector3 difference = Hero.transform.position - Rotate.transform.position;
                transform.position = new Vector2(transform.position.x, transform.position.y);
                float rot2 = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Rotate.transform.rotation = Quaternion.Euler(0f, 0f, rot2 + Offset);
            }
            
        }

        public void FireBallSpawn()
        {
            TimerShoots = 0f;
            for(int i = 0; i< SpawnFire.Length; i++)
            {
                Instantiate(FireBall, SpawnFire[i].position, Rotate.transform.rotation);
            }
        }

        void Flip()
        {
            FacingRight = !FacingRight;
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }
}
