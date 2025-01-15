using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; } //property used to separate get and set and pascal case used here

    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSys gridSystem;

    private void Awake()
    {
        gridSystem = new GridSys(10, 10, 2f); //calling the gridsys constructor here for testing since this script is attached to a game object
        //and follow MonoBehavior
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

        if (Instance != null)
        {
            Debug.Log("There's more than one LevelGrid!!!! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
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
    }
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition); //lambda expression
    //same as 
    //public GridPosition GetGridPosition(Vector3 worldPosition)
    //{
    //    return gridSystem.GetGridPosition(worldPosition);
    //}
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObj gridObject = gridSystem.GetGridObj(gridPosition);
        return gridObject.HasAnyUnit();
    }
}
