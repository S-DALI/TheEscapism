using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public class MoveToTarget : MonoBehaviour
    {
        [SerializeField] private Transform Target1,Target2;
        [SerializeField] private Transform Enemy;
        [SerializeField] private MonstersHealth EnemyHp;
        [SerializeField] private Animator Enemy_Animator;
        private bool FirstBlinkActive = true, SecondBlinkActive = true;
        void Update()
        {
            if(EnemyHp.MaxHealth*0.66f>=EnemyHp.currentHealth&& FirstBlinkActive)
            {
                FirstBlinkActive = false;
                Enemy_Animator.SetTrigger("BlinkToPoint");
                Enemy.position = new Vector2(Target1.position.x, Target1.position.y);
            }
            if (EnemyHp.MaxHealth * 0.33f >= EnemyHp.currentHealth && SecondBlinkActive)
            {
                SecondBlinkActive = false;
                Enemy_Animator.SetTrigger("BlinkToPoint");
                Enemy.position = new Vector2(Target2.position.x, Target2.position.y);
            }
        }
    }
}
