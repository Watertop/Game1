using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;
    public bool isPlayer;
    public bool isInvincible;
	// Use this for initialization
	void OnEnable () {
        currentHealth = maxHealth;
        if(gameObject.CompareTag("Player")){
            isPlayer = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(currentHealth <= 0){
			if (gameObject.CompareTag ("Enemy")) {
				GetComponent<EnemyScript> ().Death ();
			}
            gameObject.SetActive(false);
            if(isPlayer){
                //GAME OVER
            }
        }
	}
    public IEnumerator PostHitInvibility (){
        isInvincible = true;
        //flash animation
        yield return new WaitForSeconds(3);
    }

}
