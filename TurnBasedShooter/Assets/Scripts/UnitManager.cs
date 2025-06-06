using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private List<UnitMovement> unitList;
    private List<UnitMovement> playerUnitList;
    private List<UnitMovement> enemyUnitList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitManager in the scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        unitList = new List<UnitMovement>(); //initialized so functions below can access
        playerUnitList = new List<UnitMovement>();
        enemyUnitList = new List<UnitMovement>();
    }

    private void Start()
    {
        UnitMovement.OnAnyUnitSpawned += UnitMovement_OnAnyUnitSpawned;
        UnitMovement.OnAnyUnitDead += UnitMovement_OnAnyUnitDead;
    }

    private void UnitMovement_OnAnyUnitSpawned(object sender, System.EventArgs e)
    {
        UnitMovement unitMovement = sender as UnitMovement;

        unitList.Add(unitMovement);

        if (unitMovement.IsEnemy())
        {
            enemyUnitList.Add(unitMovement);
        }
        else
        {
            playerUnitList.Add(unitMovement);
        }
    }
    private void UnitMovement_OnAnyUnitDead(object sender, System.EventArgs e)
    {
        UnitMovement unitMovement = sender as UnitMovement;

        unitList.Remove(unitMovement);

        if (unitMovement.IsEnemy())
        {
            enemyUnitList.Remove(unitMovement);
        }
        else
        {
            playerUnitList.Remove(unitMovement);
        }
    }

    public List<UnitMovement> GetUnitList()
    {
        return unitList;
    }

    public List<UnitMovement> GetPlayerUnitList()
    {
        return playerUnitList;
    }

    public List<UnitMovement> GetEnemyUnitList()
    {
        return enemyUnitList;
    }
}
