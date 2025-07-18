using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGrid : MonoBehaviour
{
    [SerializeField] private UnitMovement unit;
    private void Start()
    {
    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorldPos.GetPosition());
            GridPosition startGridPosition = new GridPosition(0, 0);

            List<GridPosition> gridPositionList = Pathfinding.Instance.FindPath(startGridPosition, mouseGridPosition);

            for (int i = 0; i < gridPositionList.Count; i++)
            {
                Debug.DrawLine(LevelGrid.Instance.GetWorldPosition(gridPositionList[i]),
                               LevelGrid.Instance.GetWorldPosition(gridPositionList[i+1]),
                               Color.black,
                               10f);
            }
        }
    }
}
