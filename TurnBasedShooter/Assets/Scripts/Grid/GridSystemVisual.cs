using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    [Serializable] //needs to be done so this struct shows up in the inspector
    public struct GridVisualTypeMaterial //could be made in another file but small thing so beind done here
    {
        public GridVisualType gridVisualColourType; //type of colour for the grid visual
        public Material material; //material for the grid visual
    }

    public enum GridVisualType
    {
        White, //default colour
        Red, //for invalid positions
        Green, //for valid positions
        Yellow //for selected positions
    }

    [SerializeField] private Transform gridSystemVisualSinglePrefab;

    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList; //list of grid visual type material colors to hold different types of materials for different grid visuals

    private GridSystemVisualSingle[,] gridSystemVisualSingleArray; //making a 2D array for the visuals

    private void Awake() //awake typically used for initializing (this)
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one GridSystemVisual!!!! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this; //usecase of singleton pattern 
    }

    private void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisualSingle[
            LevelGrid.Instance.GetWidth()
            , LevelGrid.Instance.GetHeight()
            ];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridSystemVisualSingleArray[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }

        UnitSelector.Instance.OnSelectedActionChange += UnitSelector_OnSelectedActionChange;
        LevelGrid.Instance.OnAnyUnitMovedInGridPosition += LevelGrid_OnAnyUnitMovedInGridPosition;
        UpdateGridVisual(); //updating the grid visual at the start
    }

    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualColourType)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisualColourType));
        }
    }

    private void UpdateGridVisual()
    {
        HideAllGridPosition();

        BaseAction selectedAction = UnitSelector.Instance.GetSelectedAction(); //referencing the unit outside of this class so selected unit can be called below

        GridVisualType gridVisualColourType;
        switch (selectedAction)
        {
            default:
            case MoveAction moveAction:
                gridVisualColourType = GridVisualType.White; //default colour for move action
                break;
            case SpinAction spinAction:
                gridVisualColourType = GridVisualType.Green; //default colour for spin action
                break;
            case ShootAction shootAction:
                gridVisualColourType = GridVisualType.Red; //default colour for shoot action
                break;

        }
        ShowGridPositionList(selectedAction.GetValidActionGridPositionList(),
            gridVisualColourType
        ); //showing the valid action grid position list for the selected action
    }

    private void UnitSelector_OnSelectedActionChange(object sender, System.EventArgs e)
    {
        UpdateGridVisual(); //updating the grid visual when the selected action changes
    }

    private void LevelGrid_OnAnyUnitMovedInGridPosition(object sender, System.EventArgs e)
    {
        UpdateGridVisual(); //updating the grid visual when any unit is moved in the grid position
    }

    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualColourType)
    {
        foreach (GridVisualTypeMaterial gridVisualTypeMaterialColor in gridVisualTypeMaterialList)
        {
            if (gridVisualTypeMaterialColor.gridVisualColourType == gridVisualColourType)
            {
                return gridVisualTypeMaterialColor.material;
            }
        }
        Debug.LogError("Could not find material for " + gridVisualColourType);
        return null; //returning null if no material is found
    }
}
