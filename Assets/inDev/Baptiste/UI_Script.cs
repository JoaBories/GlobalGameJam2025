
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject UImanager;
    public Button play;
    public Button mainMenu;
    public Button resume;
    public Button quit;
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Image _icon1;
    public Image _icon2;
    public TextMeshProUGUI _text1;
    public TextMeshProUGUI _text2;
    public AudioSource soundSource;
    public AudioClip clickSound;
    private int premRnd;
    private int deuxRnd;
    public Sprite duck;
    public Sprite champaign;
    public Sprite sponge;
    public Sprite soap;
    public Sprite wand;

    
    private string _textDuck;
    private string _textChampaign;
    private string _textSponge;
    private string _textSoap;
    private string _textWand;

    // Start is called before the first frame update
    void Start()
    {

        if (UImanager != null) { gameManager = UImanager.GetComponent<GameManager>(); }

      if (play != null) { play.GetComponent<Button>().onClick.AddListener(Play); }
      if (resume != null) { resume.GetComponent<Button>().onClick.AddListener(Resume); }
      if (quit != null){ quit.GetComponent<Button>().onClick.AddListener(Quit); }
      if(upgradeButton1 != null){ upgradeButton1.GetComponent<Button>().onClick.AddListener(Upgrade1);}
      if(upgradeButton2 != null){  upgradeButton2.GetComponent<Button>().onClick.AddListener(Upgrade2);}

      
    

        _textDuck = "Increase enemy knockback";
        _textChampaign = "Fully heal you";
        _textSponge = "Increase your speed";
        _textSoap = "Increase damage";
        _textWand = "Increase firerate";



    }

    private void OnEnable()
    {
        if (upgradeButton1 != null && upgradeButton2 != null)
        {
            UpgradeChoose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play() { soundSource.PlayOneShot(clickSound);  SceneManager.LoadScene(1); }
    public void Resume() { soundSource.PlayOneShot(clickSound); gameManager.Unpause(); }
    public void Quit() { soundSource.PlayOneShot(clickSound);  Application.Quit(); }
    public void Upgrade1() { soundSource.PlayOneShot(clickSound); PlayerUpgrade.Instance.GetUpgrade(premRnd); }
    public void Upgrade2() { soundSource.PlayOneShot(clickSound); PlayerUpgrade.Instance.GetUpgrade(deuxRnd); }
    private void UpgradeChoose() 
    {
        premRnd = Random.Range(0,5) ;
        if (premRnd == 0) { _icon1.sprite = duck; _text1.text = _textDuck; }
        else if (premRnd == 1) { _icon1.sprite = wand; _text1.text = _textWand; }
        else if (premRnd == 2) { _icon1.sprite = champaign; _text1.text = _textChampaign; }
        else if (premRnd == 3) { _icon1.sprite = soap; _text1.text = _textSoap; }
        else { _icon1.sprite = sponge; _text1.text = _textSponge; }
        deuxRnd = Random.Range(0, 5); 
        while (deuxRnd == premRnd) { deuxRnd = Random.Range(0, 5); }
        if (deuxRnd == 0) { _icon2.sprite = duck; _text2.text = _textDuck; }
        else if (deuxRnd == 1) { _icon2.sprite = wand; _text2.text = _textWand; }
        else if (deuxRnd == 2) { _icon2.sprite = champaign; _text2.text = _textChampaign; }
        else if (deuxRnd == 3) { _icon2.sprite = soap; _text2.text = _textSoap; }
        else { _icon2.sprite = sponge; _text2.text = _textSponge; }


    }


}
