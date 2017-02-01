using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public ObjectPoolerScript bulletObjPool;
    public ObjectPoolerScript explosionObjPool;

    GameObject Player;
    public float repeatrate;
    public Transform bulletSpawn1;
    public float speed;
    public int ScoreValue;
    public GameObject Explosion;
    public string BulletPattern;
	private Camera cam;
	private Plane[] planes;
	bool canShoot;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		planes = GeometryUtility.CalculateFrustumPlanes(cam);
        Player = GameObject.Find("Player");

	}
	
	// Update is called once per frame
	void Update () {
		planes = GeometryUtility.CalculateFrustumPlanes(cam);

        TestIfInScreen();
        if (GetComponent<HealthScript>().currentHealth <= 0)
        {
            Death();
        }

        transform.Translate(Vector2.left * Time.deltaTime * speed);


    }
	void OnDisable(){
        CancelInvoke();
	}
    public void Death(){
		//Used by HealthScript
		GameControllerScript.GC.Score += Mathf.RoundToInt(ScoreValue ^ (1+ (GameControllerScript.GC.ComboInt/10)));
		GameControllerScript.GC.ComboInt += 1;
		GameControllerScript.GC.chainTimer = GameControllerScript.GC.maxChainTimer;
		GameObject Boom = explosionObjPool.GetPooledObject();
		if (Boom == null)
			return;
		Boom.transform.position = transform.position + new Vector3(0,0,-5);
		Boom.SetActive(true);
    }
	void TestIfInScreen(){
		if (GeometryUtility.TestPlanesAABB (planes, gameObject.GetComponent<Collider2D> ().bounds) && !IsInvoking(BulletPattern)) {
			if (Player.activeSelf && !IsInvoking(BulletPattern))
			{
				InvokeRepeating(BulletPattern,0, repeatrate);
			}
		}
		if (!GeometryUtility.TestPlanesAABB (planes, gameObject.GetComponent<Collider2D> ().bounds)) {
			gameObject.GetComponent<HealthScript> ().isInvincible = true;
		} else {
			gameObject.GetComponent<HealthScript> ().isInvincible = false;
		}
	
	}
    void ShootToward(){
        GameObject obj1 = bulletObjPool.GetPooledObject();
        if (obj1 == null)
            return;
        obj1.transform.position = bulletSpawn1.position;
        obj1.GetComponent<BulletScript>().Direction = (Player.transform.position-bulletSpawn1.position).normalized;
        obj1.SetActive(true);
    }
    void ShootSpread(){
        GameObject obj1 = bulletObjPool.GetPooledObject();
//        if (obj1 == null)
//            return;
        obj1.transform.position = bulletSpawn1.position;
		obj1.GetComponent<BulletScript>().Direction = new Vector2(-1,0.25f).normalized;
        obj1.SetActive(true);

        GameObject obj2 = bulletObjPool.GetPooledObject();
//        if (obj2 == null)
//            return;
        obj2.transform.position = bulletSpawn1.position;
		obj2.GetComponent<BulletScript>().Direction = new Vector2(-1,0).normalized;
        obj2.SetActive(true);

        GameObject obj3 = bulletObjPool.GetPooledObject();
//        if (obj3 == null)
//            return;
        obj3.transform.position = bulletSpawn1.position;
		obj3.GetComponent<BulletScript>().Direction = new Vector2(-1,-0.25f).normalized;
        obj3.SetActive(true);
    }
}
