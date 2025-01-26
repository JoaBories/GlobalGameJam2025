using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    private Transform player;
    private Rigidbody rb;

    private bool pause;

    [SerializeField] private float pv;
    [SerializeField] public float damage;
    [SerializeField] public int pointForKill;

    private Vector3 tempVel;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.pause)
        {
            if (rb.IsSleeping())
            {
                rb.WakeUp();
                rb.velocity = tempVel;
            }

            Vector3 diff = player.transform.position - transform.position;
            Vector2 flatDiff = new Vector2(diff.x, diff.z);
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(flatDiff.x, flatDiff.y) * Mathf.Rad2Deg, 0);

            if (!pause)
            {
                Vector3 targetMovement = transform.forward * moveSpeed;
                Vector3 movementDiff = targetMovement - rb.velocity;

                Vector3 movement = movementDiff * acceleration;

                rb.AddForce(new Vector3(movement.x, 0, movement.z), ForceMode.Force);
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

    public void Hit(float damage, Vector3 knockback)
    {
        pv -= damage;
        Debug.Log(pv);
        if (pv <= 0)
        {
            //animation yassified
            GameManager.instance.GainPoints(pointForKill);
            Destroy(gameObject);
        } 
        else
        {
            //animation hurt
            rb.AddForce(-new Vector3(knockback.x, 0, knockback.z) * 10, ForceMode.Impulse);
        }
    }

}
