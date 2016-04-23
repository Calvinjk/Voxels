using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

    public int maxEnemies;
    public bool ___________________;
    public int numEnemies;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
	}
}
