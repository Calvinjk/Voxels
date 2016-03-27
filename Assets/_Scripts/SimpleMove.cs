using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {

    public float moveSpeed = 1f;
    public float jumpheight = 1f;
    public bool _______________________________;
    public float horizontalInput;
    public float verticalInput;
    public bool hasJump = true;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
        verticalInput = Input.GetAxis("Vertical") * moveSpeed;

        GetComponent<Rigidbody>().AddForce(horizontalInput, 0f, verticalInput);

        if (Input.GetKey(KeyCode.Space) && hasJump) {
            hasJump = false;
            GetComponent<Rigidbody>().AddForce(0f, jumpheight, 0f);
        }
    }

    void OnCollisionStay(Collision coll) {
        if (coll.gameObject.tag == "Ground") {
            hasJump = true;
        }
    }
}
