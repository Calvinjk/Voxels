using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {

    public GameObject[] enemies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

    void OnTriggerEnter(Collider obj) {
        for (int i = 0; i < enemies.Length; ++i) {
            if (enemies[i].name == "Spawner") {
                Spawner spawner = (Spawner)enemies[i].GetComponent(typeof(Spawner));
                spawner.activated = true;
            } else {
                EnemyBehavior enemyBehavior = (EnemyBehavior)enemies[i].GetComponent(typeof(EnemyBehavior));
                enemyBehavior.activated = true;
            }
        }
    }
}
