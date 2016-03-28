using UnityEngine;
using System.Collections;

public class LandslideDirector : MonoBehaviour {

    public bool stopX = false;
    public bool stopY = false;
    public bool stopZ = false;
    public bool ______________;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay(Collider coll) {
        if (coll.gameObject.name == "Voxel") {
            Rigidbody r = coll.gameObject.GetComponent<Rigidbody>();
            if (stopX) {
                r.velocity = new Vector3(0f, r.velocity.y, r.velocity.z);
            }
            if (stopY) {
                r.velocity = new Vector3(r.velocity.x, 0f, r.velocity.z);
            }
            if (stopZ) {
                r.velocity = new Vector3(r.velocity.x, r.velocity.y, 0f);
            }
        }
    }
}
