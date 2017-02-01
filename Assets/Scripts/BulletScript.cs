using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    public Vector2 Direction;

    public float speed;
    public float timeUntilDeath;
    public float bulletDamage;
    public float maxTimeUntilDeath;
    public bool isPlayerBullet;

    private Plane[] planes;

	// Use this for initialization
    void Start (){
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
    }
    void OnEnable () {
        timeUntilDeath = maxTimeUntilDeath;
	}

	
	// Update is called once per frame
	void Update () {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        //Test to see if its in the game view, if not then kill this bullet
        if (!GeometryUtility.TestPlanesAABB(planes, gameObject.GetComponent<Collider2D>().bounds)){
            gameObject.SetActive(false);
        }
        //Movement
        transform.Translate(Direction * speed * Time.deltaTime);

        timeUntilDeath -= Time.deltaTime;
        if(timeUntilDeath <= 0){
            gameObject.SetActive(false);
        }
        if(PlayerScript.Player.isBombing){
            gameObject.SetActive(false);
        }
	}
    void OnTriggerEnter2D(Collider2D other){
        //print("Hit");
        if (isPlayerBullet && other.gameObject.CompareTag("Enemy") && !other.gameObject.GetComponent<HealthScript>().isInvincible)
        {
            other.gameObject.GetComponent<HealthScript>().currentHealth = Mathf.Clamp(other.gameObject.GetComponent<HealthScript>().currentHealth -= 1, 0, Mathf.Infinity);
            gameObject.SetActive(false);

        }
        if (!isPlayerBullet && other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<HealthScript>().isInvincible)
        {
            other.gameObject.GetComponent<HealthScript>().currentHealth = Mathf.Clamp(other.gameObject.GetComponent<HealthScript>().currentHealth -= 1, 0, Mathf.Infinity);
            other.gameObject.GetComponent<HealthScript>().PostHitInvibility();
            gameObject.SetActive(false);
        }
    

    }
}
