﻿using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.down * 0.7f * Time.deltaTime);
	}
}
