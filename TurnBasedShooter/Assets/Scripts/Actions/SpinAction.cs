using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    //public delegate void SpinCompleteDelegate(); //stores a function as a variable v powerful

    private float totalSpinAmount;

    private void Update()
    {
        float spinAddAmount = 360f * Time.deltaTime;
        if (!isActive)
        {
            return;
        }
        transform.eulerAngles += new Vector3(0, spinAddAmount);

        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360f) //after one rtation the unit stops spinning
        {
            isActive = false;
            onActionComplete();
        }
    }

    public void Spin(Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;
        totalSpinAmount = 0f;
    }

    public override string GetActionName()
    {
        return "Spin";
    }
}
