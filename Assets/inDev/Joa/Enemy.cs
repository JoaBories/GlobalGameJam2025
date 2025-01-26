using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static bool isAlreadyScreaming = false;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    private Transform player;
    private Rigidbody rb;

    private bool pause;

    [SerializeField] private float pv;
    [SerializeField] public float damage;
    [SerializeField] public int pointForKill;

    private Vector3 tempVel;

    private Animator animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
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

            if ((GameObject.FindGameObjectWithTag("Player").gameObject.transform.position - transform.position).sqrMagnitude <= 3.5 * 3.5 && !isAlreadyScreaming)
            {
                SoundManager.instance.PlaySound("TooClose", transform.position);
                isAlreadyScreaming = true;
                Invoke("ResetScreaming", 1f);
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

    private void ResetScreaming()
    {
        isAlreadyScreaming = false;
    }

    public void Hit(float damage, Vector3 knockback)
    {
        pv -= damage;
        Debug.Log(pv);
        if (pv <= 0)
        {
            animator.Play("yassified");
            GameManager.instance.GainPoints(pointForKill);
            if (CompareTag("Cat"))
            {
                SoundManager.instance.PlaySound("Cat", transform.position);
            }
            else
            {
                SoundManager.instance.PlaySound("Yassified", transform.position);
            }
            Destroy(rb);
            Destroy(gameObject, 3f);
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(this);
        } 
        else
        {
            animator.Play("hurt");
            rb.AddForce(-new Vector3(knockback.x, 0, knockback.z) * 10, ForceMode.Impulse);
        }
    }

}
