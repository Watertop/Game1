using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    private Transform player;
	// Use this for initialization
	void Awake () {
        player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * Time.deltaTime * 1);
	}
}
