using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour //doesn't allow creating instances of class using abstract. we only want of children
{
    protected UnitMovement unit; //no external classes can touch but classes that our extensions of this can

    protected bool isActive;

    protected Action onActionComplete;

    protected virtual void Awake() //virtual allows extensions to access this class and override it
    {
        unit = GetComponent<UnitMovement>();
    }
}
