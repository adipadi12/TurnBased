using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    [SerializeField] private UnitMovement selectedUnit;
    [SerializeField] private LayerMask unitMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleSelection()) return; //if unit selected no need to move it to raycast position on floor so it just ends function here
            //targetPos = MouseWorldPos.GetPosition(); //instead of this we have a move function defined to get targetPos defined as this
            selectedUnit.Move(MouseWorldPos.GetPosition());
        }
    } 

    private bool TryHandleSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray stored shooting a ray from the main camera to the mouse position in the game window
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitMask))
        {
            if(raycastHit.transform.TryGetComponent<UnitMovement>(out UnitMovement unitMovement))  // returns a bool value while searching for component
            {
                selectedUnit = unitMovement;
                return true;
            }

            //UnitMovement unitMovement1 = raycastHit.transform.GetComponent<UnitMovement>();  //we can use this method instead of trygetcomponent as well
            //if (unitMovement1 != null)//first checking if the raycast is hitting a component with unitmovement script or not and then checking if it's not null
            //{
            //}
        } 
        return false;
    }
}
