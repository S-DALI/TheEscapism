using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_script : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float Speed;
    [SerializeField] private float FirstPoint;
    [SerializeField] private float SecondPoint;
    void Update()
    {
        if (transform.position.y != target.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
        if (transform.position.y == FirstPoint)
        {
            target.position = new Vector3(target.position.x, SecondPoint,0);
        }
        if (transform.position.y == SecondPoint)
        {
            target.position = new Vector3(target.position.x, FirstPoint,0);
        }
    }
}
