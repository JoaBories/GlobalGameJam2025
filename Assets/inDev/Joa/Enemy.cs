using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Transform player;
    private Rigidbody rb;

    [SerializeField] private float pv;
    [SerializeField] public float damage;
    [SerializeField] public int pointForKill;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        rb.velocity = transform.forward * moveSpeed; 
    }

    private void FixedUpdate()
    {
        //Rotation
        Vector3 diff = player.transform.position - transform.position;
        Vector2 flatDiff = new Vector2(diff.x, diff.z);
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(flatDiff.x, flatDiff.y) * Mathf.Rad2Deg, 0);
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
            transform.position -= knockback;
        }
    }

}
