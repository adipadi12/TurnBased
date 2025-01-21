using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class UnitSelector : MonoBehaviour
{
    public static UnitSelector Instance { get; private set; } //property used to separate get and set and pascal case used here
    //property can only be set by this class but read by any other class
    public event EventHandler OnSelectedUnitChange; //works with any delegate but EventHandler is the C# standard

    [SerializeField] private UnitMovement selectedUnit;
    [SerializeField] private LayerMask unitMask;

    private BaseAction selectedAction;
    private bool isBusy;

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

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
        if (isBusy)
        {
            return;
        }
        if (TryHandleSelection())
        {
            return;
        }//if unit selected no need to move it to raycast position on floor so it just ends function here
         //targetPos = MouseWorldPos.GetPosition(); //instead of this we have a move function defined to get targetPos defined as this


        //selectedUnit.GetMoveAction().Move(MouseWorldPos.GetPosition());
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0)) {

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorldPos.GetPosition());

            if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedAction.TakeAction(mouseGridPosition, ClearBusy); //method using generic tick action
            }

            //switch (selectedAction)
            //{
            //    case MoveAction moveAction:
            //        if (moveAction.IsValidActionGridPosition(mouseGridPosition))
            //        {
            //            SetBusy();
            //            moveAction.Move(mouseGridPosition, ClearBusy);
            //        }
            //        break;
            //    case SpinAction spinAction:
            //        SetBusy();
            //        //selectedUnit.GetSpinAction().Spin(ClearBusy); //calling clear busy here using a delegate in spin which will restore unit's busy state after a movement us done
            //        spinAction.Spin(ClearBusy);
            //        break;
            //}
            //uses switch
        }
    }
    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    private bool TryHandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray stored shooting a ray from the main camera to the mouse position in the game window
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitMask))
            {
                if (raycastHit.transform.TryGetComponent<UnitMovement>(out UnitMovement unitMovement))  // returns a bool value while searching for component
                {
                    if (unitMovement == selectedUnit)
                    {
                        return false;
                    }
                    SetSelectedUnit(unitMovement);
                    return true;
                }

                //UnitMovement unitMovement1 = raycastHit.transform.GetComponent<UnitMovement>();  //we can use this method instead of trygetcomponent as well
                //if (unitMovement1 != null)//first checking if the raycast is hitting a component with unitmovement script or not and then checking if it's not null
                //{
                //}
            }
        }
        return false;
    }
    

    private void SetSelectedUnit(UnitMovement unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty); //? checks if null or not and then the event is invoked. does same shit as below 4 lines
        //does not care if ay event is subscribed to it or not
        //if (OnSelectedUnitChange != null)
        //{
        //    OnSelectedUnitChange(this, EventArgs.Empty);
        //}
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction; //we can access this script in the UI function now
    }

    public UnitMovement GetSelectedUnit()
    {
        return selectedUnit; //retrieving the selected unit
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
