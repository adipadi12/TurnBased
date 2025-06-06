using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        WaitiingForEnemyTurn,
        TakingTurn,
        Busy,
    }

    private State state;
    private float timer;

    private void Awake()
    {
        state = State.WaitiingForEnemyTurn; // because when initialized the enemy is waiting for the player to make their turn
    }

    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        switch (state)
        {
            case State.WaitiingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    state = State.Busy;
                    if (TryTakeEnemyAIAction(SetStateToTakingTurn))
                    {
                        state = State.Busy; // Set the state to Busy if an action was successfully taken
                    }
                    else
                    {
                        TurnSystem.Instance.NextTurn(); // If no action was taken, end the turn immediately
                    }
                }
                break;
            case State.Busy:
                // Do nothing, wait for the busy action to finish
                break;
        }
    }

    private void SetStateToTakingTurn()
    {
        timer = 0.5f;
        state = State.TakingTurn; // Set the state to TakingTurn when the enemy AI is ready to take its turn
    }
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            state = State.TakingTurn;
            timer = 2f;
        }
    }


    private bool TryTakeEnemyAIAction(Action onEnemyAiActionComplete)
    {
        foreach (UnitMovement enemyUnit in UnitManager.Instance.GetEnemyUnitList())
        {
            if (TryTakeEnemyAIAction(enemyUnit, onEnemyAiActionComplete))
            {
                return true;
            }
        }
        return false; // If no enemy units are available to take action
    }

    private bool TryTakeEnemyAIAction(UnitMovement enemyUnit, Action onEnemyAiActionComplete)
    {
        Debug.Log("Taking action for enemy unit: " + enemyUnit.name);
        SpinAction spinAction = enemyUnit.GetSpinAction();

        GridPosition actionGridPosition = enemyUnit.GetGridPosition(); // Get the grid position of the enemy unit

        if (!spinAction.IsValidActionGridPosition(actionGridPosition))
        {
            return false;
        }

        if (!enemyUnit.TrySpendActionPointsToTakeAction(spinAction)) //checking if unit has enough points to spend on a particular action
        {
            return false;
        }

        Debug.Log("Doing Spin Action");
        spinAction.TakeAction(actionGridPosition, onEnemyAiActionComplete); //method using generic tick action
        return true; // Action was successfully taken
    }
}
