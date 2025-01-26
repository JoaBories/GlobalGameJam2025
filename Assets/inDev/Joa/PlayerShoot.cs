using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Transform shootDirection;
    [SerializeField] private Transform eyes;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeSpan;
    [SerializeField] private float baseFireRate;
    [SerializeField] private float bulletOffset;
    [SerializeField] private float damage;
    [SerializeField] private float maxAmmo;
    [SerializeField] private float ammoPerSecOfReload;
    [SerializeField] private float ammoPerBullet;

    private float ammo;
    private float lastShootTimer;

    private bool shooting;

    private bool reloading;

    private void Start()
    {
        ammo = maxAmmo;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.pause)
        {

            float shootCooldown = 1/(baseFireRate + PlayerUpgrade.Instance.FirerateUpgradeAmount * PlayerUpgrade.Instance.nbOfFirerateUpgrade);

            lastShootTimer -= Time.fixedDeltaTime;

            if (shooting && lastShootTimer <= 0 && ammo >= ammoPerBullet && !reloading)
            {
                Shoot();
                lastShootTimer = shootCooldown;
            }

            if (reloading)
            {
                ammo += ammoPerSecOfReload * Time.fixedDeltaTime;
                if (ammo > maxAmmo)
                {
                    ammo = maxAmmo;
                }
            }
        }


        //Debug.Log(ammo);
    }

    private void Shoot()
    {
        GameObject currentBubble;
        currentBubble = Instantiate(bubblePrefab, eyes.position + shootDirection.forward * bulletOffset, Quaternion.identity);
        currentBubble.GetComponent<BubbleScript>().lifeSpan = bulletLifeSpan;
        currentBubble.GetComponent<BubbleScript>().damage = damage;
        currentBubble.GetComponent<Rigidbody>().AddForce(shootDirection.forward * bulletSpeed, ForceMode.Impulse);

        ammo -= ammoPerBullet;
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        Debug.Log("yo");

        if (context.started)
        {
            reloading = true;
            Debug.Log("reloading");
        }

        if (context.canceled)
        {
            reloading = false;
        }
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
