
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife Instance;

    [SerializeField] private float maxPv;
    [SerializeField] private float invincibilityCooldown;

    public float pv;

    private float invicibilityTimer;

    [SerializeField] Image lifeUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pv = maxPv;
    }

    private void Update()
    {
        lifeUI.fillAmount = pv/maxPv;
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
        if (currentEnemy.CompareTag("Enemy") || currentEnemy.CompareTag("Cat"))
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
            SoundManager.instance.PlaySound("PlayerHurt", transform.position);
            if (pv <= 0)
            {
                // animation die
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("GameOver");
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
