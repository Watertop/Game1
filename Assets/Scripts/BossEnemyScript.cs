using UnityEngine;
using System.Collections;

public class BossEnemyScript : MonoBehaviour {
    ObjectPoolerScript objPool;
    GameObject Player;
    public Transform bulletSpawn1;
    public Transform bulletSpawn2;
    public Transform bulletSpawn3;


    public int ScoreValue;
	// Use this for initialization
	void Start () {
        objPool = GetComponent<ObjectPoolerScript>();

        Player = GameObject.Find("Player");
        InvokeRepeating("ShootToward", 1, 0.35f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnDisable(){
        CancelInvoke();
        GameControllerScript.GC.Score += ScoreValue;
		GameControllerScript.GC.ComboInt += 1;
		GameControllerScript.GC.chainTimer = GameControllerScript.GC.maxChainTimer;
    }
    void ShootToward(){

        GameObject obj1 = objPool.GetPooledObject();
        if (obj1 == null)return;
        obj1.transform.position = bulletSpawn1.position;
        obj1.GetComponent<BulletScript>().Direction = new Vector2((Player.transform.position - bulletSpawn1.position).x-2, (Player.transform.position - bulletSpawn1.position).y).normalized;
        obj1.SetActive(true);

        GameObject obj2 = objPool.GetPooledObject();
        if (obj2 == null)return;
        obj2.transform.position = bulletSpawn2.position;
        obj2.GetComponent<BulletScript>().Direction = (Player.transform.position-bulletSpawn2.position).normalized;
        obj2.SetActive(true);

        GameObject obj3 = objPool.GetPooledObject();
        if (obj3 == null)return;
        obj3.transform.position = bulletSpawn3.position;
        obj3.GetComponent<BulletScript>().Direction = new Vector2((Player.transform.position - bulletSpawn3.position).x+2, (Player.transform.position - bulletSpawn1.position).y).normalized;
        obj3.SetActive(true);
    }
}
