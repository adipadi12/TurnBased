using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public UnitMovement targetUnit;
        public UnitMovement shootingUnit;
    }

    private enum State
    {
        Aiming,
        Shooting, 
        Cooloff,
    }
    private State state;
    private int maxShootDistance = 7;
    private float stateTimer;
    private UnitMovement targetUnit;
    private bool canShootBullet;

    private void Update()
    {
        float spinAddAmount = 360f * Time.deltaTime;
        if (!isActive)
        {
            return;
        }

        stateTimer -= Time.deltaTime;
        switch (state) { 
            
            case State.Aiming:
                Vector3 aimDir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
               
                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed); //smoothening of the look direction of character

                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    
                    canShootBullet = false;
                }
                break;
            case State.Cooloff: //state machine for firing system
                
                break;
        }
        if (stateTimer <= 0f)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                
                    state = State.Cooloff;
                    float coolOffStateTime = 0.5f;
                    stateTimer = coolOffStateTime;
               
                break;
            case State.Cooloff:

                ActionComplete();
                
                break;
        }

        //Debug.Log(state);
    }

    private void Shoot()
    {
        OnShoot?.Invoke(this, new OnShootEventArgs //using the extension made on the events args class to implement shooting
        {
            targetUnit = targetUnit,
            shootingUnit = unit
        });

        targetUnit.Damage();
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition > validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition; //custom struct so had to make operators allow

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance)
                {
                    continue;
                }

                //grid position empty no unit
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                UnitMovement targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }

                //adding of these 2 positions
                validGridPositionList.Add(testGridPosition);
                //Debug.Log(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        //Debug.Log("Aiming");
        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canShootBullet = true;
    }

    
}
