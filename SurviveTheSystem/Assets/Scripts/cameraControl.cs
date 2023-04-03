using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public float minAngle ,maxAngle;
    public float sensX, sensY;
    public bool lockYRot;
    public float yLockMin, yLockMax;

    public Transform orientation;

    float xRot, yRot;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        getMouseInput();
        controlCamera();
    }

    void getMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRot += mouseX;
        xRot -= mouseY;

    }

    public void controlCamera()
    {

        xRot = Mathf.Clamp(xRot, minAngle, maxAngle);

        if (lockYRot)
        {
            yRot = Mathf.Clamp(yRot, yLockMin, yLockMax);
        }

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }

}
