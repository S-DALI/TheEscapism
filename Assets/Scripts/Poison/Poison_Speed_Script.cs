using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets { 
public class Poison_Speed_Script : MonoBehaviour
{
    [SerializeField] private Hero Speed;
    [SerializeField] private AudioSource PotionSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Hero"))
        {
            Speed.speed += 1f;
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
