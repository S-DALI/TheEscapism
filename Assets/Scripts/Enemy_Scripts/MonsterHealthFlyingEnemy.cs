using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Assets
{
    
    public class MonsterHealthFlyingEnemy : MonstersHealth
    {
        [SerializeField] private AIPath canmove;
        [SerializeField] private Transform PlayerPosition;
        [SerializeField] private float AgroDistanceFlyingEnemy;
        protected override void Update()
        {
            float DistanceToPlayer = Vector2.Distance(transform.position,PlayerPosition.position);
            if (HPBar.gameObject.activeSelf == true)
            {
                TimerDisappear += Time.deltaTime;
                if (TimerDisappear >= DisappearHpBar)
                {
                    TimerDisappear = 0;
                    HPBarDisappear.SetTrigger("Disappear");
                }
            }
            if (DistanceToPlayer > AgroDistanceFlyingEnemy)
                canmove.canMove = false;
            else
                canmove.canMove = true;
        }
        public override void Die()
        {
            Hero.Instance.CurrentKill++;
            this.enabled = false;
            HealthChangedEvent();
            Monster.SetBool("Bandit_leave", true);
            HPBar.gameObject.SetActive(false);
        }
        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, AgroDistanceFlyingEnemy);
        }
    }
}
