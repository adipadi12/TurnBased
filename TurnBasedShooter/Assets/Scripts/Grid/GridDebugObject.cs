using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textMeshPro;

    private GridObj gridObject;
    public void SetGridObject(GridObj gridObject)   //another constructor for grid object setting
    { 
        this.gridObject = gridObject;
    }

    private void Update()
    {
        textMeshPro.text = gridObject.ToString(); //updating the values inside textmeshpro to get shit done
    }
}
