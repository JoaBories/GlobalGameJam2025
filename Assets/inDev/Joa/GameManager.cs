using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int point;        //keep between scene
    public static int nbOfUpgrades; //same and this one just for math

    private float timer;
    private int timerSimple;

    public bool pause;
    public GameObject pauseMenu;
    public GameObject playerUI;
    public GameObject upgradeUI;
   

    [SerializeField] private float endTime;
    [SerializeField] private GameObject pointDisplay;
    [SerializeField] private GameObject timerDisplay;
    

    [SerializeField] private int pointByUpgrade;

    [SerializeField] private string nextSceneName;

    private void Awake()
    {
        instance = this;

        timer = endTime;

        if (upgradeUI != null) { upgradeUI.SetActive(false); }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause)
            {
                Unpause();
            } else
            {
                Pause();
                pauseMenu.SetActive(true);
                playerUI.SetActive(false);
            }
        }

        if (!pause)
        {
            if (upgradeUI != null)
            {
                pauseMenu.SetActive(false);
                playerUI.SetActive(true);
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    End();
                }
                timerSimple = (int) timer;
            }
            pointDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + point; // pointDisplay
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "Time : " + timerSimple;// timerDisplay
        }
    }

    public void GainPoints(int gain)
    {
        point += gain;

        if (nbOfUpgrades * pointByUpgrade <= point)
        {
            nbOfUpgrades++;
            GetUpgrade();
        }
    }

    private void GetUpgrade()
    {
        playerUI.SetActive(false);
        upgradeUI.SetActive(true);
        Pause();
    }

    private void End()
    {
        Debug.Log("no more time");
        SceneManager.LoadScene(nextSceneName);
    }

    public void Pause()
    {
        pause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unpause()
    {
        pause = false;
        playerUI.SetActive(true);
        upgradeUI.SetActive(false);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
