using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	public GameObject Enemy;
    public float spawnRate;
	// Use this for initialization
	void Start () {
        InvokeRepeating ("Spawn", 1, spawnRate);
	}
	
	// Update is called once per frame
	void Update () {

	}
//	IEnumerator Spawn(){
//
//		yield return new WaitForSeconds (1f);
//	}
	void Spawn(){
        //print("spawn");
		GameObject NewEnemy = Instantiate (Enemy);
		NewEnemy.transform.position = gameObject.transform.position;

	}
}
