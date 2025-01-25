using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class BubbleScript : MonoBehaviour
{
    public float lifeSpan;
    public float lifeTime;
    public float damage;
    private Rigidbody rb;

    [SerializeField] private List<int> layersNumberForPop;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        lifeTime += Time.fixedDeltaTime;
        if (lifeTime >= lifeSpan)
        {
            Pop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (layersNumberForPop.Contains(other.gameObject.layer))
        {
            Pop();
        }
        if (other.transform.parent != null)
        {
            if (other.transform.parent.CompareTag("Enemy"))
            {
                other.transform.parent.gameObject.GetComponent<Enemy>().Hit(damage);
                Pop();
            }
        }
    }

    private void Pop()
    {
        Destroy(gameObject);
    }
}
