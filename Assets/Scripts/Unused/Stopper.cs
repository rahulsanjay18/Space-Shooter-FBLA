﻿using UnityEngine;
using System.Collections;

public class Stopper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        
    }
}
