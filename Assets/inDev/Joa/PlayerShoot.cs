using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] Transform shootDirection;
    [SerializeField] Transform eyes;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeSpan;
    [SerializeField] float shootCooldown;
    [SerializeField] float bulletOffset;
    [SerializeField] float damage;

    float lastShootTimer;

    private bool shooting;

    private void FixedUpdate()
    {
        lastShootTimer -= Time.fixedDeltaTime;

        if (shooting && lastShootTimer <= 0)
        {
            Shoot();
            lastShootTimer = shootCooldown;
        }
    }

    private void Shoot()
    {
        GameObject currentBubble;
        currentBubble = Instantiate(bubblePrefab, eyes.position + shootDirection.forward * bulletOffset, Quaternion.identity);
        currentBubble.GetComponent<BubbleScript>().lifeSpan = bulletLifeSpan;
        currentBubble.GetComponent<BubbleScript>().damage = damage;
        currentBubble.GetComponent<Rigidbody>().AddForce(shootDirection.forward * bulletSpeed, ForceMode.Impulse);
    }

    public void OnFirePressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shooting = true;
        }

        if (context.canceled)
        {
            shooting = false;
        }

    }
}
