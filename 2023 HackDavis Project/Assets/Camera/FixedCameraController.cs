using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraController : AbstractCameraController
{
    private Camera managedCamera;
    private void Awake()
    {
        managedCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        var targetPosition = this.Target.transform.position;
        var cameraPosition = managedCamera.transform.position;

        this.managedCamera.transform.position = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);
    }
}
