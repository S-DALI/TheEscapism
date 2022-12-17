using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
   
    public class Wall : WallBoss
    {
        [SerializeField]private int NeedKill;
        protected override void Update()
        {
            if(Hero.Instance.CurrentKill>=NeedKill)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
