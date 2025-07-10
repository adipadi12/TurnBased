using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelGrid : MonoBehaviour
{
    public event EventHandler OnAnyUnitMovedInGridPosition; //event to notify when any unit is moved in the grid position
    public static LevelGrid Instance { get; private set; } //property used to separate get and set and pascal case used here

    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSys<GridObj> gridSystem;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one LevelGrid!!!! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gridSystem = new GridSys<GridObj>(10, 10, 2f,
            (GridSys<GridObj> g, GridPosition gridPosition) => new GridObj(g, gridPosition)); //calling the gridsys constructor here for testing since this script is attached to a game object
                                                                                      //and follow MonoBehavior
        //gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPos(GridPosition gridPosition, UnitMovement unit)
    {
        GridObj gridObject = gridSystem.GetGridObj(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<UnitMovement> GetUnitListAtGridPos(GridPosition gridPosition)
    {
        GridObj gridObject = gridSystem.GetGridObj(gridPosition);
        return gridObject.GetUnit();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, UnitMovement unit)
    {
        GridObj gridObject = gridSystem.GetGridObj(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(UnitMovement unit, GridPosition fromGridPosiiton, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosiiton,unit);
        AddUnitAtGridPos(toGridPosition, unit);

        OnAnyUnitMovedInGridPosition?.Invoke(this, EventArgs.Empty); //null conditional operator to check if there are any subscribers to the event
    }
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition); //lambda expression
    //same as 
    //public GridPosition GetGridPosition(Vector3 worldPosition)
    //{
    //    return gridSystem.GetGridPosition(worldPosition);
    //}
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public int GetWidth() => gridSystem.GetWidht();
    public int GetHeight() => gridSystem.GetHeight(); //exposing height and width from grid system so it can be used by our grid selected visual script
    //to cycle through the grid
    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObj gridObject = gridSystem.GetGridObj(gridPosition);
        return gridObject.HasAnyUnit();
    }

    public UnitMovement GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObj gridObject = gridSystem.GetGridObj(gridPosition);
        return gridObject.GetUnitListIndex();
    }
}
