using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysLookAtCam : MonoBehaviour
{
    [SerializeField] private bool invert;

    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (invert)
        {
            Vector3 dirToCamera = (cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + dirToCamera * -1); //to account for inverted text
        }
        else
        {
            transform.LookAt(cameraTransform);
        }
    }
}
