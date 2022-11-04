using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] private Queen_Run RunScript;
        [SerializeField] private MonstersHealth LeftAngelHP;
        [SerializeField] private MonstersHealth RightAngelHP;
        void Update()
        {
            if (QueenSave)
            {
                attackTimer += Time.deltaTime;

                if (PlayerinSign() && PlayerHP.HP > 0)
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
            
        }

        private void QueenAttack()
        {
            if (attackTimer >= attaclCoolDawn && HP_Queen.currentHealth > 200)
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
    }
}

