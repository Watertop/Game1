using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenuControllerScript : MonoBehaviour {
    public int buttonValue = 0; 
    public GameObject button0;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Up")){
            if(buttonValue > 0){
                buttonValue -= 1;
            }

        }
        if(Input.GetButtonDown("Down")){
            if(buttonValue < 4){
                buttonValue += 1;
            }

        }
        if(Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1")){
            buttonValue = 0;
        }
        if(Input.GetButtonDown("Cancel") || Input.GetButtonDown("Fire2")){
            buttonValue = 0;
        }

        if(buttonValue == 0){
            button0.SetActive(true);
        }else{
            button0.SetActive(false);
        }
        if(buttonValue == 1){
            button1.SetActive(true);
        }else{
            button1.SetActive(false);
        }
        if(buttonValue == 2){
            button2.SetActive(true);
        }else{
            button2.SetActive(false);
        }
        if(buttonValue == 3){
            button3.SetActive(true);
        }else{
            button3.SetActive(false);
        }


 


	}
}
