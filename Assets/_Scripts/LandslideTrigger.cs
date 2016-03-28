using UnityEngine;
using System.Collections;

public class LandslideTrigger : MonoBehaviour {

    public GameObject[] shatterCubes;
    public GameObject[] enemies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.name == "Player") {
            //Trigger the landslide
            for (int i = 0; i < shatterCubes.Length; ++i) {
                Shatter script = (Shatter)shatterCubes[i].GetComponent(typeof(Shatter));
                script.Die();
            }

            //Cause enemies to walk to player
            for (int i = 0; i < enemies.Length; ++i) {
                EnemyBehavior script = (EnemyBehavior)enemies[i].GetComponent(typeof(EnemyBehavior));
                script.activated    = true;
                script.moving       = true;
            }
        }
    }
}
