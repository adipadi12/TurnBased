using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    //public delegate void SpinCompleteDelegate(); //stores a function as a variable v powerful

    private float totalSpinAmount;

    private void Update()
    {
        float spinAddAmount = 360f * Time.deltaTime;
        if (!isActive)
        {
            return;
        }
        transform.eulerAngles += new Vector3(0, spinAddAmount);

        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360f) //after one rtation the unit stops spinning
        {
            ActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete) //grid position isn't used but since it is abstract in the base class and overriden here
    {
        totalSpinAmount = 0f;
        ActionStart(onActionComplete);
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition> { 
            unitGridPosition 
        };
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }
}