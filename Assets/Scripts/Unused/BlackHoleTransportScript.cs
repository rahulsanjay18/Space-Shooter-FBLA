﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlackHoleTransportScript : MonoBehaviour {

    public string newLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        
        SceneManager.LoadScene(newLevel, LoadSceneMode.Single);
    }
}
