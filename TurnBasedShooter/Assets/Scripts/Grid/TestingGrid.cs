using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGrid : MonoBehaviour
{
    [SerializeField] private UnitMovement unit;
    private void Start()
    {
     //   gridSystem = new GridSys(10,10, 2f); //calling the gridsys constructor here for testing since this script is attached to a game object
     //   //and follow MonoBehavior
     //   gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
     ////   Debug.Log(new GridPosition(5, 7));
    }

    private void Update()
    {
        //Debug.Log(gridSystem.GetGridPosition(MouseWorldPos.GetPosition())); //calling the function to getGridPosition of where the mouse is in the world
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            //GridSystemVisual.Instance.ShowGridPositionList(
            //    unit.GetMoveAction().GetValidActionGridPositionList(), GridSystemVisual.GridVisualType.White );
        }
    }
}
