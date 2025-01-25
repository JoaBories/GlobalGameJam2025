using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float maxPv;
    [SerializeField] private float invincibilityCooldown;

    public float pv;

    private float invicibilityTimer;

    private void Start()
    {
        pv = maxPv;
    }

    private void FixedUpdate()
    {
        invicibilityTimer -= Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
        {
            GameObject currentEnemy = other.transform.parent.gameObject;
            if (currentEnemy.CompareTag("Enemy"))
            {
                Hit(currentEnemy.GetComponent<Enemy>().damage);
            }
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
}
