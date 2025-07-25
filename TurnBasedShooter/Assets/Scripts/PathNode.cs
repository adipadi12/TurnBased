using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition gridPosition;
    private int gCost; // Cost from start node
    private int hCost; // Heuristic cost to end node
    private int fCost; // Total cost (gCost + hCost)
    private PathNode cameFromPathNode; // Node from which this node was reached

    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();  //overriding to string so x and z values can be used by text mesh pro
    }

    public int GetGCost()
    {
        return gCost;
    }

    public int GetHCost()
    {
        return hCost;
    }

    public int GetFCost()
    {
        return fCost;
    }

    public void SetGCost(int gCost)
    {
        this.gCost = gCost;
    }

    public void SetHCost(int hCost)
    {
        this.hCost = hCost;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost; // Update fCost whenever gCost or hCost changes
    }

    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null; // Reset the path node from which this node was reached
    }

    public void SetCameFromPathNode(PathNode pathNode)
    {
        cameFromPathNode = pathNode; // Set the path node from which this node was reached
    }

    public PathNode GetCameFromPathNode()
    {
        return cameFromPathNode; // Return the path node from which this node was reached
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition; // Return the grid position of this node
    }
}
