﻿using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Escape)) {
            print("Tried to quit.");
            Application.Quit();
        }
	}
}