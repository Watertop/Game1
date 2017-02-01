using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour {
    HealthScript Health;
    public GameObject HealthBarImage;
    private float orginalXValue;
	// Use this for initialization
    void Start(){
    }
    void OnEnable () {
        
        Health = gameObject.GetComponent<HealthScript>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float currenthealth = Health.currentHealth / Health.maxHealth;
        HealthBarImage.transform.localScale = new Vector3 (currenthealth , HealthBarImage.transform.localScale.y, HealthBarImage.transform.localScale.z);
        if (Health.currentHealth < Health.maxHealth * 0.5f){
            HealthBarImage.GetComponent<Image>().color = Color.yellow;
        }
        if (Health.currentHealth < Health.maxHealth * 0.25f){
            HealthBarImage.GetComponent<Image>().color = Color.red;
        }
	}


}
