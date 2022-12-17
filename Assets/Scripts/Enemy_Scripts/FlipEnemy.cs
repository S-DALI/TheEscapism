using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemy : MonoBehaviour
{
    [SerializeField]private Transform PlayerPosition;
    [SerializeField] private Animator SmokeAnim;


    void Update()
    {
        if(PlayerPosition.position.x<transform.position.x && transform.localScale.x>0 || PlayerPosition.position.x > transform.position.x && transform.localScale.x<0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void SmokeAnimation()
    {
        SmokeAnim.SetTrigger("WormDead");
    }
}
