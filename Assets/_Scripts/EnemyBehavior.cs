using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public enum EnemyType {Swarmer};

    public EnemyType enemyType;
    public Material[] possibleColors;
    public bool _______________;
    public bool activated   = false;
    public bool moving      = false;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material = possibleColors[Random.Range(0, possibleColors.Length)];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (activated) {
            if (moving) {
                WalkTowardsPlayer();
            }
        }
	}

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.name == "Player") {
            Die();
        }
    }

    void WalkTowardsPlayer() {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        GetComponent<NavMeshAgent>().SetDestination(playerPos);
    }

    void Die() {
        switch (enemyType) {
            case EnemyType.Swarmer:
                Shatter shatter = (Shatter)GetComponent(typeof(Shatter));
                shatter.Die();
                break;
            default:
                //Give all children a rigidbody and un-child it
                for (int i = 0; i <= transform.childCount; ++i) {
                    transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    transform.GetChild(i).parent = null;
                    KillSelf killSelf = (KillSelf)transform.GetChild(i).gameObject.GetComponent(typeof(KillSelf));
                    killSelf.startTimer = true;
                    if (transform.GetChild(i).name == "Body") {
                        Shatter s = (Shatter)transform.GetChild(i).gameObject.GetComponent(typeof(Shatter));
                        s.Die();
                    }
                }

                Destroy(this.gameObject);
                break;
        }

    }
}
