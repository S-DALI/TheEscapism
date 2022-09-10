using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit_Run : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D BanditRB;
    public float speed = 2.5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Hero").transform;
        BanditRB = animator.GetComponent<Rigidbody2D>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x,BanditRB.position.y);
        Vector2 NewPos = Vector2.MoveTowards(BanditRB.position, target, speed * Time.fixedDeltaTime);
        BanditRB.MovePosition(NewPos);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

