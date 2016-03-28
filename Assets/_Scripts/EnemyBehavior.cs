using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float moveSpeed  = 1f;
    public bool _______________;
    public bool activated   = false;
    public bool moving      = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (activated) {
            if (moving) {
                WalkTowardsPlayer();
            }
        }
	}

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.name == "Voxel") {
            Die();
        }
    }

    void WalkTowardsPlayer() {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        transform.LookAt(playerPos);

        Vector3 directionVector = playerPos - transform.position;
        GetComponent<Rigidbody>().AddForce(directionVector.normalized * moveSpeed);
    }

    void Die() {
        //Give all children a rigidbody and un-child it
        for (int i = 0; i <= transform.childCount; ++i) {
            transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            transform.GetChild(i).parent = null;
            if (transform.GetChild(i).name == "Body") {
                Shatter script = (Shatter)transform.GetChild(i).gameObject.GetComponent(typeof(Shatter));
                script.Die();
            }
        }

        Destroy(gameObject);
    }
}
