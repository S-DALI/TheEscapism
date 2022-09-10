using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public class Enemy_Patrol : MonoBehaviour
    {
        [Header("Patrol Points")]
        [SerializeField] private Transform LeftPoint;
        [SerializeField] private Transform RightPoint;

        [Header("Enemy")]
        [SerializeField] private Transform Enemy;
        [SerializeField] private Rigidbody2D EnemyRB;
        [SerializeField] private MonstersHealth HP;
        [SerializeField] private int NumberHpPoision;
        [SerializeField] private Animator SmokeBlink;
        [Header("Move Parametrs")]
        [SerializeField] private float speed;
        [SerializeField] private float IDLEDuration;
        private float TimerIDLE;
        private Vector3 initScale;
        private bool MovingLeft=true;
        [SerializeField] private float BattleDistance;
        [Header("Enemy Animator")]
        [SerializeField] Animator EnemyAnimator;
        [Header("Player Position")]
        [SerializeField] private Transform player;
        [SerializeField] private float AgroDistance;
        [SerializeField] private Hero PlayerHP;

        private void Awake()
        {
            initScale = Enemy.localScale;
        }
        private void Start()
        {
        }

        private void Update()
        {
            if (HP.currentHealth > 0)
            {
                float DistanceToPlayer = Vector2.Distance(Enemy.position, player.position);
                if (AgroDistance >= DistanceToPlayer && player.position.x > LeftPoint.position.x && player.position.x < RightPoint.position.x && LeftPoint.position.x <= Enemy.position.x && RightPoint.position.x >= Enemy.position.x )
                {
                    StartChase();
                }
                else
                {
                    Patrol();
                }
            }
        }
        private void DirectionChange()
        {
            EnemyAnimator.SetTrigger("IDLE");
            TimerIDLE += Time.deltaTime;
            if(TimerIDLE>IDLEDuration)
                MovingLeft = !MovingLeft;
        }
        private void MoveDirection(int Direction)
        {
            TimerIDLE = 0;
            EnemyAnimator.SetTrigger("Patrol");
            Enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * Direction, initScale.y, initScale.z);
            Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime * speed * Direction, Enemy.position.y, Enemy.position.z);
        }

        private void Patrol()
        {
            if (MovingLeft)
            {

                if (LeftPoint.position.x <= Enemy.position.x)
                {
                    MoveDirection(-1);
                }
                else
                {
                    DirectionChange();
                }
            }
            else
            {
                if (RightPoint.position.x >= Enemy.position.x)
                {
                    MoveDirection(1);
                }
                else
                {
                    DirectionChange();
                }
            }
        }

        private void StartChase()
        {
            float DistanceToPlayer = Vector2.Distance(Enemy.position, player.position);
            if (PlayerHP.HP>0) 
            {
                
                if (player.position.x < Enemy.transform.position.x && DistanceToPlayer >= BattleDistance)
                {   EnemyAnimator.SetTrigger("Patrol");
                    Enemy.transform.position = Vector2.MoveTowards(Enemy.position, player.position, speed * Time.deltaTime * 1.5f);
                    if (Enemy.transform.localScale.x < 0)
                        Enemy.transform.localScale = new Vector2(Enemy.transform.localScale.x, Enemy.localScale.y);
                    else Enemy.transform.localScale = new Vector2(-Enemy.transform.localScale.x, Enemy.localScale.y);

                }
                else
                {
                    if (player.position.x > Enemy.transform.position.x && DistanceToPlayer >= BattleDistance)
                    {
                        EnemyAnimator.SetTrigger("Patrol");
                        Enemy.transform.position = Vector2.MoveTowards(Enemy.position, player.position, speed * Time.deltaTime * 1.5f);
                        if (Enemy.transform.localScale.x > 0)
                            Enemy.transform.localScale = new Vector2(Enemy.transform.localScale.x, Enemy.localScale.y);
                        else Enemy.transform.localScale = new Vector2(-Enemy.transform.localScale.x, Enemy.localScale.y);
                    }
                }
                if (DistanceToPlayer <= BattleDistance )
                {
                    CombatMode();
                }
            }
            else
            {
                EnemyAnimator.SetTrigger("IDLE");
                EnemyRB.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            }
           
        }

        private void CombatMode()
        {
            EnemyAnimator.SetTrigger("BattleIDLE");
            if(player.position.x < Enemy.transform.position.x)
            {
                if (Enemy.transform.localScale.x < 0)
                    Enemy.transform.localScale = new Vector2(Enemy.transform.localScale.x, Enemy.localScale.y);
                else Enemy.transform.localScale = new Vector2(-Enemy.transform.localScale.x, Enemy.localScale.y);
                Smoke(-0.7f);
            }
            if(player.position.x > Enemy.transform.position.x)
            {
                if (Enemy.transform.localScale.x > 0)
                    Enemy.transform.localScale = new Vector2(Enemy.transform.localScale.x, Enemy.localScale.y);
                else Enemy.transform.localScale = new Vector2(-Enemy.transform.localScale.x, Enemy.localScale.y);
                Smoke(0.7f);
            }

        }


        private void Smoke(float DistanceSmoke)
        {
            if (HP.currentHealth > 0 && HP.currentHealth <= 25 && NumberHpPoision > 0)
            {
                SmokeBlink.SetTrigger("blink");
                Enemy.transform.position = new Vector2(player.transform.position.x+DistanceSmoke,Enemy.position.y);
                HP.currentHealth += 50;
                NumberHpPoision--;
                Enemy.transform.localScale = new Vector2(-Enemy.transform.localScale.x, Enemy.localScale.y);
                
            }
        }
    }
}