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
        EnemyAIAction bestEnemyAIAction = null; // Initialize the best action to null
        BaseAction bestBaseAction = null; // Initialize the best base action to null

        foreach (BaseAction baseAction in enemyUnit.GetBaseActionArray())
        {
            if (!enemyUnit.CanSpendActionPointsToTakeAction(baseAction)) //checking if unit has enough points to spend on a particular action
            {
                continue; // If the unit cannot spend action points for this action, skip to the next action
            }

            if (bestEnemyAIAction == null)
            {
                bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                bestBaseAction = baseAction; // Store the best base action if it's the first one found
            }

            else
            {
                EnemyAIAction testEnemyAIAction = baseAction.GetBestEnemyAIAction(); // Get the best action for the enemy AI from the base action
                if ((testEnemyAIAction != null) && testEnemyAIAction.actionValue > bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                    bestBaseAction = baseAction; // Update the best base action if a better action is found
                }
            }
        }

        if (bestEnemyAIAction != null && enemyUnit.TrySpendActionPointsToTakeAction(bestBaseAction))
        {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition, onEnemyAiActionComplete); // Use the best action's grid position to take the action
            return true; // Action was successfully taken
        }
        else
        {
            return false;
        }
    }
}
