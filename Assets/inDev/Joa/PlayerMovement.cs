using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseMoveSpeed;
    [SerializeField] Transform orientation;
    [SerializeField] float groundDrag;

    private Vector2 moveInput;
    private Vector3 moveDirection;

    private Rigidbody rb;

    private float moveSpeed;

    private float stepTimer;

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

            if (moveInput != Vector2.zero)
            {
                stepTimer -= Time.fixedDeltaTime;
                if (stepTimer < 0)
                {
                    SoundManager.instance.PlaySound("Step", transform.position);
                    stepTimer = Random.Range(0.4f, 0.6f);
                }
            }

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
