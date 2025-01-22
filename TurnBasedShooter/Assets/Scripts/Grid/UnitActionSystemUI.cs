using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonList;

    private void Awake()
    {
        actionButtonList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitSelector.Instance.OnSelectedUnitChange += UnitSelector_OnSelectedUnitChange; //there was already
        //an event that reads changing of unit on selection. we used that event and created an action function
        //in which we pass the same function of creating a unit action button
        UnitSelector.Instance.OnSelectedActionChange += UnitSelector_OnSelectedActionChange;
        UnitSelector.Instance.OnActionStarted += UnitSelector_OnActionStarted;

        UpdateActionPoints();
        CreateUnitActionButton();
        UpdateSelectedVisual();
    }

    private void CreateUnitActionButton()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject); //destroys the game object first checking how many buttons to instantiate depending on unit
        }

        actionButtonList.Clear();

        UnitMovement selectedUnit = UnitSelector.Instance.GetSelectedUnit(); //fetching the unit selector function

        foreach(BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform); //spawns new button at transform of container
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);

            actionButtonList.Add(actionButtonUI);
        }
    }

    private void UnitSelector_OnSelectedUnitChange(object sender, EventArgs e) 
    {
        CreateUnitActionButton();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void UnitSelector_OnSelectedActionChange(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void UnitSelector_OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButtonUI in actionButtonList)
        {
            actionButtonUI.UpdateSelectedVisual(); //updates the border of the button
        }
    }

    private void UpdateActionPoints()
    {
        UnitMovement selectedUnit = UnitSelector.Instance.GetSelectedUnit();
        actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints(); //updates action points
    }
}
