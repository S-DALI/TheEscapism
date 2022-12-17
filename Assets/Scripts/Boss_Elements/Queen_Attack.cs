using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Assets
{
    public class Queen_Attack : MeleeEnemy
    {
        private int currentAttack;
        [SerializeField] private MonstersHealth HP_Queen;
        [SerializeField] private Transform QueenTransform;
        [SerializeField] private GameObject Fire_Shield;
        [SerializeField] private Transform Point_Save;
        [SerializeField] private GameObject Platform_Boss;
        [SerializeField] private GameObject AngelLeft;
        [SerializeField] private GameObject AngelRight;
        private bool QueenSave = true;
        private bool Queen2Stage = false;
        private bool Proced_Battle = false;
        [SerializeField] private Queen_Run RunScript;
        [SerializeField] private MonstersHealthAngelLeft LeftAngelHP;
        [SerializeField] private MonstersHealthAngelLeft RightAngelHP;
        [SerializeField] private Transform SpawnFireRain;
        private Vector2 Spawn;
        [SerializeField] private float SpawnRate;
        private float NextSpawn;
        [SerializeField] private GameObject FireRain;
        [SerializeField] private Transform HeroPosition;
        void Update()
        {
            attackTimer += Time.deltaTime;
            if (QueenSave)
            {
                if (PlayerinSign() && PlayerHP.HP > 0 && HP_Queen.currentHealth > 200)
                {
                    QueenAttack();
                }
                if (HP_Queen.currentHealth <= 200 && QueenSave)
                {
                    QueenSave = false;
                    RunScript.enabled = false;
                    HP_Queen.currentHealth += 100;
                    HP_Queen.enabled = false;
                    Fire_Shield.active = true;
                    Platform_Boss.active = true;
                    AngelLeft.active = true;
                    AngelRight.active = true;
                    range = 0;
                    QueenTransform.position = new Vector3(Point_Save.position.x, Point_Save.position.y, 0);
                    Anim.SetTrigger("Under_Protection");
                }
            }

            if(LeftAngelHP.AngelDie && RightAngelHP.AngelDie && QueenSave == false)
            {
                Queen2Stage = true;
                Proced_Battle = true;
                
            }

            if(Queen2Stage && Proced_Battle)
            {
                Anim.SetTrigger("Proceed_Battle");
                HP_Queen.enabled = true;
                RunScript.enabled = true;
                range = 1;
            }
            if(Queen2Stage)
            {
                NextSpawn += Time.deltaTime;

                if (PlayerinSign() && PlayerHP.HP > 0)
                {
                    QueenAttack();
                }
                if(NextSpawn>=SpawnRate && HP_Queen.currentHealth>0 && !PlayerinSign())
                {
                    Anim.SetTrigger("FireRain");
                }
            }
            
        }

        private void QueenAttack()
        {
            if (attackTimer >= attaclCoolDawn )
            {
                    currentAttack+=1;
                    if (currentAttack > 3)
                    {
                        currentAttack = 1;
                    }
                    if (attackTimer > 2f)
                    {
                        currentAttack = 1;
                    }
                    Anim.SetTrigger("Attack"+currentAttack);
                    attackTimer = 0;
            }
        }

        private void QueenAttackFly()
        {
            if (attackTimer >= attaclCoolDawn+2f && HP_Queen.currentHealth > 0 && PlayerinSign())
            {
                Anim.SetTrigger("AttackFly");
                attackTimer = 0;
            }
        }

        public void FireRainSpawn()
        {
            NextSpawn = 0f;
            Spawn = new Vector2(HeroPosition.position.x, SpawnFireRain.transform.position.y);
            Instantiate(FireRain, Spawn, Quaternion.identity);
        }
    }
}

