using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int point;        //keep between scene
    public static int nbOfUpgrades; //same and this one just for math

    private float timer;

    public bool pause;

    [SerializeField] private float endTime;
    [SerializeField] private GameObject pointDisplay;
    [SerializeField] private GameObject timerDisplay;

    [SerializeField] private int pointByUpgrade;

    [SerializeField] private string nextSceneName;

    private void Awake()
    {
        instance = this;

        timer = endTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            Debug.Log(pause);
        }

        if (!pause)
        {


            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                End();
            }

            // pointDisplay
            // timerDisplay
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
        Debug.Log("upgrade Time");
    }

    private void End()
    {
        Debug.Log("no more time");
        SceneManager.LoadScene(nextSceneName);
    }

}
