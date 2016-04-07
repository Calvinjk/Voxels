using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {

    public float rotationDegreePerSecond = 120f;
    public float speedMultiplier = 2f;
    public float turnSpeed = .8f;
    public GameObject gamecam;

    public bool ________________;

    public float speed = 0f;
    float horizontal = 0f;
    float vertical = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
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

        //Give the character some velocity if hes moving
        GetComponent<Rigidbody>().velocity = transform.forward.normalized * speed * speedMultiplier;
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

        //Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
        //Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
        //Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), inputDirection, Color.blue);
        //Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), axisSign, Color.red);

        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

        return angleRootToMove;
    }

    private bool IsInLocomotion() {
        return GetComponent<Rigidbody>().velocity != Vector3.zero;
    }
}
