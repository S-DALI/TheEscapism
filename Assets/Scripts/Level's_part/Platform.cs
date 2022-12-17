using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float Speed;
    [SerializeField] private float FirstPoint;
    [SerializeField] private float SecondPoint;
    void Update()
    {
        if(transform.position.x != target.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
        if(transform.position.x == FirstPoint)
        {
            target.position = new Vector2(SecondPoint, target.position.y);
        }
        if (transform.position.x == SecondPoint)
        {
            target.position = new Vector2(FirstPoint, target.position.y);
        }
    }
}
