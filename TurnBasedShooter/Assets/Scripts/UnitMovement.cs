using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitMovement : MonoBehaviour
{
    private Vector3 targetPos;
    public float moveSpeed = 4f;
    public float stoppingValue = 0.1f;
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) > stoppingValue)// setting an offset for stopping distance
        {
            Vector3 moveDir = (targetPos - transform.position).normalized; //we just need the direction so we normalize it we don't need magnitude

            transform.position += moveDir * moveSpeed * Time.deltaTime; //updating transform.positon frame independently
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            
           
                Move(new Vector3(0, 0, 5));
                Debug.Log("Moving");
            
        }
    }
    private void Move(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
}
