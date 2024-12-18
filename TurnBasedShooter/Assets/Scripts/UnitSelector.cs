using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitSelector : MonoBehaviour
{
    public static UnitSelector Instance { get; private set; } //property used to separate get and set and pascal case used here
    //property can only be set by this class but read by any other class
    public event EventHandler OnSelectedUnitChange; //works with any delegate but EventHandler is the C# standard

    [SerializeField] private UnitMovement selectedUnit;
    [SerializeField] private LayerMask unitMask;

    private void Awake() //awake typically used for initializing (this)
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one UnitSelector!!!! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this; //usecase of singleton pattern 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleSelection()) return; //if unit selected no need to move it to raycast position on floor so it just ends function here
            //targetPos = MouseWorldPos.GetPosition(); //instead of this we have a move function defined to get targetPos defined as this
            selectedUnit.GetMoveAction().Move(MouseWorldPos.GetPosition());
        }
    } 

    private bool TryHandleSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray stored shooting a ray from the main camera to the mouse position in the game window
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitMask))
        {
            if(raycastHit.transform.TryGetComponent<UnitMovement>(out UnitMovement unitMovement))  // returns a bool value while searching for component
            {
                SetSelectedUnit(unitMovement);
                return true;
            }

            //UnitMovement unitMovement1 = raycastHit.transform.GetComponent<UnitMovement>();  //we can use this method instead of trygetcomponent as well
            //if (unitMovement1 != null)//first checking if the raycast is hitting a component with unitmovement script or not and then checking if it's not null
            //{
            //}
        } 
        return false;
    }

    private void SetSelectedUnit(UnitMovement unit)
    {
        selectedUnit = unit;
        
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty); //? checks if null or not and then the event is invoked. does same shit as below 4 lines
        //does not care if ay event is subscribed to it or not
        //if (OnSelectedUnitChange != null)
        //{
        //    OnSelectedUnitChange(this, EventArgs.Empty);
        //}
    }

    public UnitMovement GetSelectedUnit()
    {
        return selectedUnit; //retrieving the selected unit
    }
}
