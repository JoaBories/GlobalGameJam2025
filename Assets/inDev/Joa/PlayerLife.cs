using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife Instance;

    [SerializeField] private float maxPv;
    [SerializeField] private float invincibilityCooldown;

    public float pv;

    private float invicibilityTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pv = maxPv;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.pause)
        {
            invicibilityTimer -= Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject currentEnemy = other.gameObject;
        if (currentEnemy.CompareTag("Enemy"))
        {
            Hit(currentEnemy.GetComponent<Enemy>().damage);
        }
    }

    private void Hit(float damage)
    {
        if (invicibilityTimer <= 0)
        {
            pv -= damage;
            Debug.Log(pv);
            if (pv <= 0)
            {
                // animation die
                Debug.Log("die");
            }
            else
            {
                // animatio hurt
                invicibilityTimer = invincibilityCooldown;
            }
        }
    }

    public void Heal(float amount)
    {
        pv += amount;
        if (pv > maxPv)
        {
            pv = maxPv;
        }
    }
}
