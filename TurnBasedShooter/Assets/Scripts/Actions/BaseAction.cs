using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour //doesn't allow creating instances of class using abstract. we only want of children
{
    public static event EventHandler OnAnyActionStarted; //static so that it can be accessed without instantiating the class. EventHandler is a delegate type that defines a method signature for handling events.
    public static event EventHandler OnAnyActionCompleted; //event for when any action is completed


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

        OnAnyActionStarted?.Invoke(this, EventArgs.Empty); //if any action started then invoke the event
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();

        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty); //if any action completed then invoke the event
    }

    public UnitMovement GetUnit() //to get the unit that this action is attached to
    {
        return unit;
    }
}
