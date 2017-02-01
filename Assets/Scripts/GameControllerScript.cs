using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameControllerScript : MonoBehaviour {

    public GameObject Player;
    public int Score;

    //UI
    public Text LifeText;
    public Text ScoreText;
    public int Bombs;
    public Text GameOverScoreText;
	public Text ChainText;
    public GameObject GameOverPane;
    public Text GameOverText;

	public bool chainActive = false;
	public float maxChainTimer;
	public float chainTimer = 0;
	public int ComboInt;
	public GameObject ComboBar;



    public static GameControllerScript GC;
	// Use this for initialization
    void Awake(){
        if (GC == null) {

            DontDestroyOnLoad (transform.gameObject);
            GC = this;
        }
        else if (GC != this)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Slowdown();
		ComboChain ();
        GUI();
        if(!Player.activeInHierarchy){
            GameOver();
        }
	}
    void GUI(){
        ScoreText.text = Score.ToString();
        LifeText.text = "Lives:\n"+Player.GetComponent<HealthScript>().currentHealth.ToString();

    }
    void GameOver(){
        GameOverScoreText.text = Score.ToString(); 
        GameOverPane.SetActive(true);
        Time.timeScale = 0;
    }
	void ComboChain(){
		
		if (chainTimer > 0) {
            chainTimer -= Time.deltaTime/Time.timeScale;
			ChainText.gameObject.SetActive(true);
			ChainText.text = ComboInt.ToString () + "\nHIT";
			ComboBar.transform.localScale = new Vector3 (chainTimer/maxChainTimer , ComboBar.transform.localScale.y, ComboBar.transform.localScale.z);
		} else {
			ChainText.gameObject.SetActive(false);
			ComboInt = 0;
		}
	}
    void Slowdown(){
        if(Input.GetButton("Fire3")){
            Time.timeScale = 0.5f;
        }else{
            Time.timeScale = 1;

        }
    }

}
