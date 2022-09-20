using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class FireScript : MonoBehaviour
    {
        [SerializeField] private float TimerHPHeal=0;
        [SerializeField] private float CoolDawnHPHeal;
        [SerializeField] private Hero HeroHP;
        private void Update()
        {
            if(TimerHPHeal>=6)
            {
                TimerHPHeal = 0;
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        { 
            TimerHPHeal += Time.deltaTime;
            if (collision.gameObject.name.Equals("Hero"))
            {
                if(HeroHP.HP<10 && HeroHP.HP>0 && TimerHPHeal>=CoolDawnHPHeal)
                {
                    TimerHPHeal = 0;
                    HeroHP.HP++;
                }
            }
        }
        
    }
}

