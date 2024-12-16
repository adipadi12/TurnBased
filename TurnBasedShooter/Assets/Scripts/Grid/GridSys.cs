using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSys //Monobehavior removed because we need constructor for grid system. can't be done when monobehav present
{
    private int width;
    private int height;
    private float cellSize;
    private GridObj[,] gridObjectArray; //creating a 2D array
    public GridSys(int width, int height, float cellSize) //constructor to create an object. done by monobehavior using unity but we make our own here
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize; //this keyword used for referencing each instance of variable relevant to this class

        gridObjectArray = new GridObj[width, height]; //defined that 2D array with our width and height

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                gridObjectArray[x,z] =  new GridObj(this, gridPosition); //this being grid system. as the constructor was defined in the GridObj script
                //Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * 0.2f, Color.blue, 1000f); //drawing lines in debug mode inside a for loop
                //with width & height as params and an offset of till where you want the line to be drawn
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) //to change grid coordinates to real world coordinates
    {
        return new Vector3(gridPosition.x, 0 , gridPosition.z) * cellSize; //gets world position by multiplying grid size with cell size
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)); //gets grid position by dividing world pos by cell size
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity); //identity means no roatation
                    //here Instantiate cannot be directly used because the class is inherited from MonoBehavior
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObj(gridPosition));
            }
        }
    }

    public GridObj GetGridObj(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }
}
