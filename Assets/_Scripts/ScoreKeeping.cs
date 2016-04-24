using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreKeeping : MonoBehaviour {

    public List<float> myList = new List<float>();


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
