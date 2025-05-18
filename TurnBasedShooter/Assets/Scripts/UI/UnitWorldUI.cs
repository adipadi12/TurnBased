using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPoints;
    [SerializeField] private UnitMovement unitMovement;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;
    private void Start()
    {
        UnitMovement.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        healthSystem.OnDamage += Health_OnDamage;

        HealthBarUpdate();
        UpdateActionPoints();
    }

    private void UpdateActionPoints()
    {
        actionPoints.text = unitMovement.GetActionPoints().ToString();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    private void HealthBarUpdate()
    {
        healthBarImage.fillAmount = healthSystem.GetNormalizedHealth();
    }

    private void Health_OnDamage(object sender, EventArgs e)
    {
        HealthBarUpdate();
    }
}
