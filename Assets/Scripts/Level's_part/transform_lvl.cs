using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform_lvl : MonoBehaviour
{
    [SerializeField] private GameObject Disactive_floor;
    [SerializeField] private GameObject Active_floor;
    [SerializeField] private AudioSource SoundPotion;
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject PlatformTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Hero"))
        {
            Disactive_floor.active = false;
            Active_floor.active = true;
            Platform.active = true;
            PlatformTrigger.active = true;
            SoundPotion.Play();
            StartCoroutine(TimeWait());
        }
    }
    IEnumerator TimeWait()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
      
