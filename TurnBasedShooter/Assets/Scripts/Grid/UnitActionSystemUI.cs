using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    private void Start()
    {
        UnitSelector.Instance.OnSelectedUnitChange += UnitSelector_OnSelectedUnitChange; //there was already
        //an event that reads changing of unit on selection. we used that event and created an action function
        //in which we pass the same function of creating a unit action button
        CreateUnitActionButton();
    }

    private void CreateUnitActionButton()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject); //destroys the game object first checking how many buttons to instantiate depending on unit
        }

        UnitMovement selectedUnit = UnitSelector.Instance.GetSelectedUnit(); //fetching the unit selector function

        foreach(BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform); //spawns new button at transform of container
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
        }
    }

    private void UnitSelector_OnSelectedUnitChange(object sender, EventArgs e) 
    {
        CreateUnitActionButton();
    }
}
