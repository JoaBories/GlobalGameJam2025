using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseMoveSpeed;
    [SerializeField] Transform orientation;
    [SerializeField] float groundDrag;

    private Vector2 moveInput;
    private Vector3 moveDirection;

    private Rigidbody rb;

    private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.pause)
        {
            moveSpeed = (baseMoveSpeed + (PlayerUpgrade.Instance.SpeedUpgradeAmount * PlayerUpgrade.Instance.nbOfSpeedUpgrade)) * 10;

            moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
            rb.AddForce(moveDirection.normalized * moveSpeed);
        
            rb.drag = groundDrag;

            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);

            if (flatVelocity.sqrMagnitude > moveSpeed * moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }

            if (rb.IsSleeping())
            {
                rb.WakeUp();
            }
        }
        else
        {
            rb.Sleep();
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
