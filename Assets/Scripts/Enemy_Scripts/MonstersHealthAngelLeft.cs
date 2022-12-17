using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public class MonstersHealthAngelLeft : MonstersHealth
    {
        public bool AngelDie = false;
        public void Destroy()
        {
            AngelDie = true;
            Destroy(this.gameObject);
        }
    }
}
