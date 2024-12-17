using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stoppingValue = 0.1f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private Animator animator;

    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPos;
    private UnitMovement unit;

    private void Awake()
    {
        unit = GetComponent<UnitMovement>();
        targetPos = transform.position;
    }
    // Start is called before the first frame update
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
    }

    public void Move(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition; //custom struct so had to make operators allow
                //adding of these 2 positions
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
