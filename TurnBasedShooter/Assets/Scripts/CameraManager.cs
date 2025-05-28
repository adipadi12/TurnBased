using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCameraGO;

    private void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;

        HideActionCamera(); //hides the action camera at the start of the game
    }

    private void ShowActionCamera()
    {
        actionCameraGO.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCameraGO.SetActive(false);
    }

    private void BaseAction_OnAnyActionStarted(object sender, System.EventArgs e) //subscribing to the event from BaseAction class
    {
        switch (sender)
        {
            case ShootAction shootAction:  //shows the action camera only when the shootaction is done. here if we want to make any other sort of camera for other actions can be done as well
                Vector3 actionCameraHeight = Vector3.up * 1.5f; //setting the height of the action camera

                UnitMovement shooterUnit = shootAction.GetUnit();
                UnitMovement targetUnit = shootAction.GetTargetUnit();

                Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;

                float shoulderOffsetAmount = 0.5f; //amount to offset the camera to the shoulder of the shooter
                Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDir * shoulderOffsetAmount; //offset for the camera to be at the shoulder of the shooter

                Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + actionCameraHeight + shoulderOffset + (shootDir * -1);

                actionCameraGO.transform.position = actionCameraPosition; //setting the position of the action camera
                actionCameraGO.transform.LookAt(targetUnit.GetWorldPosition() + actionCameraHeight); //making the camera look at the target unit

                ShowActionCamera();
                break;
        }
    }

    private void BaseAction_OnAnyActionCompleted(object sender, System.EventArgs e) //subscribing to the event from BaseAction class
    {
        switch (sender)
        {
            case ShootAction shootAction:  //hides the action camera only when the shootaction is done. 
                HideActionCamera();
                break;
        }
    }
}
