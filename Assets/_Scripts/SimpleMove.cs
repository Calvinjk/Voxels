using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {

    public float moveSpeed = 1f;
    public bool _______________________________;
    public float horizontalInput;
    public float verticalInput;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
        verticalInput = Input.GetAxis("Vertical") * moveSpeed;

        GetComponent<Rigidbody>().AddForce(horizontalInput, 0f, verticalInput);
    }
}
