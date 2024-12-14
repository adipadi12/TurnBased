using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitMovement : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField]private float moveSpeed = 4f;
    [SerializeField]private float stoppingValue = 0.1f;
    [SerializeField] private float rotateSpeed = 0.5f;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        targetPos = transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) > stoppingValue)// setting an offset for stopping distance
        {
            Vector3 moveDir = (targetPos - transform.position).normalized; //we just need the direction so we normalize it we don't need magnitude

            transform.position += moveDir * moveSpeed * Time.deltaTime; //updating transform.positon frame independently
            transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //smoothening of the look direction of character

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        
        //if (Input.GetKeyDown(KeyCode.T))
        //{
            
           
        //        Move(new Vector3(0, 0, 5));
        //        Debug.Log("Moving");
            
        //}
        
    }
    public void Move(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
}
