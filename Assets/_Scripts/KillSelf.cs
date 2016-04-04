using UnityEngine;
using System.Collections;

public class KillSelf : MonoBehaviour {

    public float maxTimeAlive = 0f;
    public float minTimeAlive = 0f;
    public bool startTimer = false;
    public bool ________________;
    public float curTimeAlive = 0f;
    public float killTime;

	// Use this for initialization
	void Start () {
        killTime = Random.Range(minTimeAlive, maxTimeAlive);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (startTimer) {
            curTimeAlive += Time.deltaTime;
        }
        if (curTimeAlive > killTime) {
            Destroy(gameObject);
        }
    }
}
