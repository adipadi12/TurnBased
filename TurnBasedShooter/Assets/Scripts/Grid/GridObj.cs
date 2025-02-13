using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObj
{
    private GridSys gridSystem;
    private GridPosition gridPosition;  //private references to other grid scripts
    private List<UnitMovement> unitList;

    public GridObj(GridSys gridSystem, GridPosition gridPosition) //making a gridobject constructor with these parameters
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<UnitMovement>();
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (UnitMovement unit in unitList)
        {
            unitString += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;  //overriding to string so x and z values can be used by text mesh pro
    }

    public void AddUnit(UnitMovement unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(UnitMovement unit)
    {
        unitList.Remove(unit);
    }

    public List<UnitMovement> GetUnit()
    {
        return unitList;
    }

    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    public UnitMovement GetUnitListIndex()
    {
        if (HasAnyUnit())
        {
            return unitList[0];
        }
        else
        {
            return null;
        }
    }
}