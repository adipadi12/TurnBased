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

    private void Start()
    {
        UnitSelector.Instance.OnSelectedUnitChange += UnitSelector_OnSelectedUnitChange;

        UpdateVisual();
    }

    private void UnitSelector_OnSelectedUnitChange(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
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
}
