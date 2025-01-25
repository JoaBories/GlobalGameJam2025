using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public Button play;
    public Button mainMenu;
    public Button resume;
    public Button quit;
    public Button upgrade;
    // Start is called before the first frame update
    void Start()
    {
        play.GetComponent<Button>().onClick.AddListener(Play);
        mainMenu.GetComponent<Button>().onClick.AddListener(MainMenu);
        resume.GetComponent<Button>().onClick.AddListener(Resume);
        quit.GetComponent<Button>().onClick.AddListener(Quit);
        upgrade.GetComponent<Button>().onClick.AddListener(Upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Play() { SceneManager.LoadScene(1); }
    private void MainMenu() { SceneManager.LoadScene(0); }
    private void Resume() { }
    private void Quit() { Application.Quit(); }
    private void Upgrade() { }


}
