using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

    public event EventHandler OnTurnChanged;

    private int turnNumber = 1;

    private void Awake() //awake typically used for initializing (this)
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one TurnSystem!!!! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this; //usecase of singleton pattern 
    }
    public void NextTurn()
    {
        turnNumber++;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }
}
