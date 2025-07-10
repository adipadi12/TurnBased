using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridSys<TGridObject> //Monobehavior removed because we need constructor for grid system. can't be done when monobehav present
{
    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridObjectArray; //creating a 2D array
    public GridSys(int width, int height, float cellSize, Func<GridSys<TGridObject>, GridPosition, TGridObject> createGridObject) //constructor to create an object. done by monobehavior using unity but we make our own here
    { //bypassing C# limitations by using Func delegate to create a grid object. 
        this.width = width;
        this.height = height;
        this.cellSize = cellSize; //this keyword used for referencing each instance of variable relevant to this class

        gridObjectArray = new TGridObject[width, height]; //defined that 2D array with our width and height

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                gridObjectArray[x,z] =  createGridObject(this, gridPosition); //this being grid system. as the constructor was defined in the TGridObject script
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
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();  //getting the grid debug object component
                gridDebugObject.SetGridObject(GetGridObj(gridPosition) as GridObj);   //setting grid object as get grid object's object position
            }
        }
    }

    public TGridObject GetGridObj(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];  //getting the grid object to get us the x and z position for debugging
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 && 
               gridPosition.z >= 0 && 
               gridPosition.x < width && 
               gridPosition.z < height;
    }

    public int GetWidht()
    {
        return width;
    }
    public int GetHeight() 
        { 
        return height;
    }
}
