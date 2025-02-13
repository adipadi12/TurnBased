using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour //doesn't allow creating instances of class using abstract. we only want of children
{
    protected UnitMovement unit; //no external classes can touch but classes that our extensions of this can

    protected bool isActive;

    protected Action onActionComplete;

    protected virtual void Awake() //virtual allows extensions to access this class and override it
    {
        unit = GetComponent<UnitMovement>();
    }

    public abstract string GetActionName(); //enforce every action will implement this function

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public bool IsValidActionGridPosition(GridPosition gridPosition) 
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition); //checking if the value is present in the list or not
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual int GetActionPointsCost() //action points cost made virtual so overriden in spin and move class
    {
        return 1;
    }
    
    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
    }
}
