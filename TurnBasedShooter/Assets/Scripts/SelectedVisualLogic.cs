using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisualLogic : MonoBehaviour
{
    [SerializeField]private UnitMovement unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() //start used when have to give a reference to objects outside of the class
        //helps avoid errors between calling of Start and Awake
    {
        UnitSelector.Instance.OnSelectedUnitChange += UnitSelector_OnSelectedUnitChange; //subscribing to event

        UpdateVisual();
    }

    private void UnitSelector_OnSelectedUnitChange(object sender, EventArgs empty) //creating the subscriber
    {
        UpdateVisual();
    }

    private void UpdateVisual() //added to another function to make sure it runs during the start of the script
    {
        if (UnitSelector.Instance.GetSelectedUnit() == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnDestroy()
    {
        UnitSelector.Instance.OnSelectedUnitChange -= UnitSelector_OnSelectedUnitChange; //unsubscribing from event when enemy is destroyed so no compilation errors occur

    }
}
