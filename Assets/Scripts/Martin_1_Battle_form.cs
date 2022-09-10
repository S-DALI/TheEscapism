using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Martin_1_Battle_form : MonoBehaviour
    { 
        [Header("Enemy")]
        [SerializeField] private Transform Enemy;
        [SerializeField] private Rigidbody2D EnemyRB;
        [SerializeField] private MonstersHealth EnemyHP;
        [SerializeField] private float agroDistance;
        [Header("Parametrs")]
        [SerializeField] private float speed;
        [Header("EnemyAnimator")]
        [SerializeField] private Animator EnemyAnimator;
        [Header("Player")]
        [SerializeField] private Transform player;
        [SerializeField] private Hero PlayerHP;
        void Start()
        {

        }

        void Update()
        {
            if(EnemyHP.currentHealth>0)
            {
                float DistanceToPlayer = Vector2.Distance(Enemy.position, player.position);
                if(agroDistance>=DistanceToPlayer)
                {
                    StartChase();
                }
                if(agroDistance<DistanceToPlayer)
                {
                    EnemyAnimator.SetTrigger("IDLE");
                }
            }
        }
        private void StartChase()
        {
            float DistanceToPlayer = Vector2.Distance(Enemy.position, player.position);
            if (PlayerHP.HP > 0)
            {
                if (player.position.x < Enemy.transform.position.x)
                {
                    if (Enemy.transform.localScale.x < 0)
                        Enemy.transform.localScale = new Vector2(Enemy.transform.localScale.x, Enemy.localScale.y);
                    else Enemy.transform.localScale = new Vector2(-Enemy.transform.localScale.x, Enemy.localScale.y);
                }
                if (player.position.x > Enemy.transform.position.x)
                {
                    if (Enemy.transform.localScale.x > 0)
                        Enemy.transform.localScale = new Vector2(Enemy.transform.localScale.x, Enemy.localScale.y);
                    else Enemy.transform.localScale = new Vector2(-Enemy.transform.localScale.x, Enemy.localScale.y);
                }
                if (DistanceToPlayer > 4f)
                {
                    EnemyAnimator.SetTrigger("Patrol");
                    Enemy.transform.position = Vector2.MoveTowards(Enemy.position, player.position, speed * Time.deltaTime );

                }else
                    if(DistanceToPlayer<=4)
                {
                    EnemyAnimator.SetTrigger("IDLE");
                }
                if(DistanceToPlayer>agroDistance)
                {
                    EnemyAnimator.SetTrigger("IDLE");
                }
                //else
                //{
                //    if (player.position.x > Enemy.transform.position.x && DistanceToPlayer > 4f)
                //    {
                //        EnemyAnimator.SetTrigger("Patrol");
                //        Enemy.transform.position = Vector2.MoveTowards(Enemy.position, player.position, speed * Time.deltaTime);
                //        if (Enemy.transform.localScale.x > 0)
                //            Enemy.transform.localScale = new Vector2(Enemy.transform.localScale.x, Enemy.localScale.y);
                //        else Enemy.transform.localScale = new Vector2(-Enemy.transform.localScale.x, Enemy.localScale.y);
                //    }
                //    else EnemyAnimator.SetTrigger("IDLE");
                //}


            }
        }
    }
}
