using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private float cameraMaxAngle = 90;
    [SerializeField] private float cameraMinAngle = -90;

    [SerializeField] private Transform orientation;

    float xRotation;
    float yRotation;

    float mouseX;
    float mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!GameManager.instance.pause)
        {
            yRotation += mouseX * Time.deltaTime * sensX;
        
            xRotation -= mouseY * Time.deltaTime * sensY;
            xRotation = Mathf.Clamp(xRotation, cameraMinAngle, cameraMaxAngle);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    public void Onlook(InputAction.CallbackContext context)
    {
        mouseX = context.ReadValue<Vector2>().x;
        mouseY = context.ReadValue<Vector2>().y;
    }
}
