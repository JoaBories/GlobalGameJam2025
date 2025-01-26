using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    public float lifeSpan;
    public float lifeTime;
    public float damage;
    public float baseBnockback;
    private Rigidbody rb;

    private Vector3 tempVel;

    [SerializeField] private GameObject bubbleHit;

    [SerializeField] private List<int> layersNumberForPop;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.pause)
        {
            lifeTime += Time.fixedDeltaTime;
            if (lifeTime >= lifeSpan)
            {
                Pop();
            }

            if (rb.IsSleeping())
            {
                rb.WakeUp();
                rb.velocity = tempVel;
            }
        }
        else
        {
            if (!rb.IsSleeping())
            {
                tempVel = rb.velocity;
            }
            rb.Sleep();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (layersNumberForPop.Contains(other.gameObject.layer))
        {
            Pop();
        }
        if (other.CompareTag("Enemy") || other.CompareTag("Cat"))
        {
            other.gameObject.GetComponent<Enemy>().Hit(damage + (PlayerUpgrade.Instance.nbOfDamageUpgrade * PlayerUpgrade.Instance.DamageUpgradeAmount), -rb.velocity.normalized * (baseBnockback + PlayerUpgrade.Instance.nbOfKnockbackUpgrade * PlayerUpgrade.Instance.KnockbackUpgradeAmount));
            PopEnemy();
        }
    }

    private void PopEnemy()
    {
        SoundManager.instance.PlaySound("BubbleHit", transform.position, 0.5f);
        Destroy(Instantiate(bubbleHit, transform.position, Quaternion.identity), 1f);
        Destroy(gameObject);
    }

    private void Pop()
    {
        Destroy(Instantiate(bubbleHit, transform.position, Quaternion.identity), 0.5f);
        Destroy(gameObject);
    }
}
