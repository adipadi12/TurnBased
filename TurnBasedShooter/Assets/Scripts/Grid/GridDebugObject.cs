using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textMeshPro;

    private object gridObject;
    public virtual void SetGridObject(object gridObject)   //another constructor for grid object setting
    { 
        this.gridObject = gridObject;
    }

    protected virtual void Update()
    {
        textMeshPro.text = gridObject.ToString(); //updating the values inside textmeshpro to get shit done
    }
}
