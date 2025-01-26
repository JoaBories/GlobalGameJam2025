using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [SerializeField] private Image ammoUI;

    private float ammo;
    private float lastShootTimer;

    private bool shooting;

    private bool reloading;

    [SerializeField] private Image R_hand;
    [SerializeField] private Image L_hand;
    [SerializeField] private Image Shoot_hand;
    [SerializeField] private Image CrossHair;

    [SerializeField] private Sprite restR_hand;
    [SerializeField] private Sprite shootR_hand;
    [SerializeField] private Sprite reloadL_hand;
    [SerializeField] private Sprite reloadR_hand;


    private void Start()
    {
        ammo = maxAmmo;
    }

    private void Update()
    {
        ammoUI.fillAmount = ammo/maxAmmo;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.pause)
        {

            float shootCooldown = 1/(baseFireRate + PlayerUpgrade.Instance.FirerateUpgradeAmount * PlayerUpgrade.Instance.nbOfFirerateUpgrade);

            lastShootTimer -= Time.fixedDeltaTime;

            if (reloading)
            {
                ammo += ammoPerSecOfReload * Time.fixedDeltaTime;
                if (ammo > maxAmmo)
                {
                    ammo = maxAmmo;
                }

                R_hand.gameObject.SetActive(true);
                CrossHair.gameObject.SetActive(false);
                R_hand.sprite = reloadR_hand;
                L_hand.gameObject.SetActive(true);
                Shoot_hand.gameObject.SetActive(false);
            }
            else if (shooting)
            {
                Shoot_hand.gameObject.SetActive(true);
                CrossHair.gameObject.SetActive(false);
                R_hand.gameObject.SetActive(false);
                L_hand.gameObject.SetActive(false);

                if (lastShootTimer <= 0)
                {
                    if (ammo >= ammoPerBullet)
                    {
                        Shoot();
                        SoundManager.instance.PlaySound("Shoot", transform.position);
                        lastShootTimer = shootCooldown;
                    }
                    else
                    {
                        SoundManager.instance.PlaySound("ShootEmpty", transform.position);
                    }
                }
            }
            else
            {
                R_hand.gameObject.SetActive(true);
                CrossHair.gameObject.SetActive(true);
                R_hand.sprite = restR_hand;
                L_hand.gameObject.SetActive(false);
                Shoot_hand.gameObject.SetActive(false);
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
            SoundManager.instance.PlaySound("Reloading", transform.position);
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
