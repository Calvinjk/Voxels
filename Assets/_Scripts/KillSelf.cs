using UnityEngine;
using System.Collections;

public class KillSelf : MonoBehaviour {

    public float timeAlive = 0f;
    public bool ________________;
    public float curTimeAlive = 0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        curTimeAlive += Time.deltaTime;
        if (curTimeAlive > timeAlive) {
            Destroy(gameObject);
        }
    }
}
