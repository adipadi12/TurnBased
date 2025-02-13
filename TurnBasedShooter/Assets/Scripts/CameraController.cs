using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 18f;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;         //present in the packages of the cinemachine library we use transposer so we use this


    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();
        HandleCameraZoom();
    }

    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDir.x = +1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDir.x = -1f;
        }
        float moveSpeed = 5f;

        Vector3 moveVector = transform.forward * inputDir.z + transform.right * inputDir.x;  //to account for rotation of game object
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation()
    {
        //movement for cameracontroller

        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;  //handling rotation of the camera
    }

    private void HandleCameraZoom()
    {
        float zoomAmount = 1f;

        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET); //clamping the zoom value between these 2 so it doesn't
                                                                                                            //exceed the zoom more than required
        float zoomSpeed = 5f;
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
        //smoothens the camera zoom 
        //cinemachineTransposer.m_FollowOffset = followOffset;
    }
}
