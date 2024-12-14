using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorldPos : MonoBehaviour
{
    private static MouseWorldPos instance;

    [SerializeField] private LayerMask floorMask;

    private void Awake()
    {
        instance = this;
    }

    //private void Update()
    //{
    //    //Debug.Log(Input.mousePosition); //returns pixel coordinates of the mouse in the game window

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray stored shooting a ray from the main camera to the mouse position in the game window
    //    Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, floorMask); //returns true or false if anything with a collider is being hit or not
    //    //also defined the max distance till where raycast is done and floormask added from the inspector to detect only floor
    //    transform.position = raycastHit.point;  //changes glowing sphere's position to where the raycast is hitting in the game window
    //}

    public static Vector3 GetPosition()  //static because belongs to the class and not to any instance of it
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.floorMask); //floorMask wasn't able to be accessed with this function so an instance of
        //our MouseWorldPos script was created referencing which floorMask was enabled for access

        return raycastHit.point;
    }
}
