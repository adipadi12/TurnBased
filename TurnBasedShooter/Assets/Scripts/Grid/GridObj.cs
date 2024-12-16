using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObj
{
    private GridSys gridSystem;
    private GridPosition gridPosition;

    public GridObj(GridSys gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }
}