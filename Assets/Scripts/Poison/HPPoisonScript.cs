using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets {
    public class HPPoisonScript : MonoBehaviour
    {
        [SerializeField] private Hero HeroHP;
        [SerializeField] private AudioSource PotionSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.name.Equals("Hero") && HeroHP.HP>0 &&HeroHP.HP<10)
            {
                HeroHP.HP += 2;
                PotionSound.Play();
                StartCoroutine(TimeWait());
                //Destroy(this.gameObject);
            }
        }
        IEnumerator TimeWait()
        {
            yield return new WaitForSeconds(0.2f);
            Destroy(this.gameObject);
        }
    }
}
