using UnityEngine;
using System.Collections;

public class KillSelf : MonoBehaviour {

    public float timeAlive = 0f;
    public bool startTimer = false;
    public bool ________________;
    public float curTimeAlive = 0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (startTimer) {
            curTimeAlive += Time.deltaTime;
        }
        if (curTimeAlive > timeAlive) {
            Destroy(gameObject);
        }
    }
}
