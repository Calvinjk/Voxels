using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {

    public float rotationDegreePerSecond = 120f;
    public float speedMultiplier = 2f;
    public float turnSpeed = .8f;
    public float jumpPower = 1f;
    public GameObject gamecam;

    public bool ________________;

    float distanceToGround;
    float speed = 0f;
    float horizontal = 0f;
    float vertical = 0f;
    Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionStay(Collision obj) {

    }

    void FixedUpdate() {
        //Pull values
        horizontal  = Input.GetAxis("Horizontal");
        vertical    = Input.GetAxis("Vertical");

        //Translate values into world/player/cam space
        float angleToRotate = AngleNeededToRotate(this.transform, gamecam.transform, ref speed);

        //Rotate character
        if (horizontal != 0 || vertical != 0) {
            //Normalize this angle
            if      (angleToRotate < -1f)   { angleToRotate = -1f; }
            else if (angleToRotate > 1f)    { angleToRotate = 1f; }
            else                            { angleToRotate = 0f; }

            Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * angleToRotate, 0f), turnSpeed);
            Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
            this.transform.rotation = (this.transform.rotation * deltaRotation);
        }

        //Give the character some velocity if hes moving.  Had to do it like this to keep gravity a thing.
        Vector3 movementAttempt = transform.forward.normalized * speed * speedMultiplier;
        rigid.velocity = new Vector3(movementAttempt.x, rigid.velocity.y, movementAttempt.z);

        //Shall we jump?
        if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
            rigid.AddForce(new Vector3(0f, jumpPower * 100000f, 0f));
        }
    }

    public float AngleNeededToRotate(Transform root, Transform camera, ref float speedOut) {
        Vector3 rootDirection = root.forward;
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;  //normalized so that speed is same turning as moving forward
        speedOut = inputDirection.sqrMagnitude;

        //Get camera rotation
        Vector3 cameraDirection = camera.forward;
        cameraDirection.y = 0.0f; //kill Y (input is 2d)
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

        //Convert input into Worldspace coordinates
        Vector3 moveDirection = referentialShift * inputDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

        return angleRootToMove;
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.2f);  //The 0.1f is to account for irregularities in the ground or possibly slopes.
    }
}
