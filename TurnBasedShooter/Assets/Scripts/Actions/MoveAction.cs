using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;


    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stoppingValue = 0.1f;
    [SerializeField] private float rotateSpeed = 5f;

   //[SerializeField] private Animator animator; reference moved to unit animator class

    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPos;
    
    protected override void Awake()
    {
       // unit = GetComponent<UnitMovement>();
        base.Awake(); //calls the awake function from the base class first and then can override it to use targetPos
        targetPos = transform.position;
    }
    // Start is called before the first frame update
    private void Update()
    {
        if (!isActive)
        {
            return;
        } 
        Vector3 moveDir = (targetPos - transform.position).normalized; //we just need the direction so we normalize it we don't need magnitude

        if (Vector3.Distance(transform.position, targetPos) > stoppingValue)// setting an offset for stopping distance
        {

            transform.position += moveDir * moveSpeed * Time.deltaTime; //updating transform.positon frame independently

        }
        else
        {

            OnStopMoving?.Invoke(this, EventArgs.Empty);

            ActionComplete(); //so the logic of no active actions present works here as well
        }

        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //smoothening of the look direction of character

    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.targetPos = LevelGrid.Instance.GetWorldPosition(gridPosition);

        OnStartMoving?.Invoke(this, EventArgs.Empty); //invoking this event 
        
        ActionStart(onActionComplete);
    }


    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition; //custom struct so had to make operators allow
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    //unit can't move into the same position where it already is
                    continue;
                }
                //unit can't move into grid where other unit is already present
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                //adding of these 2 positions
                validGridPositionList.Add(testGridPosition);
                //Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName() //overriding the abstract function defined in baseaction class to get strings of each movement
    {
        return "Move";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = unit.GetShootAction().GetTargetCount(gridPosition); 
        // Spin action does not have a specific enemy AI action, so we return null or an empty action.
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCountAtGridPosition * 10, // or some default value
        };
    }
}
