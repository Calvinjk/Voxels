using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public float distanceAway;
    public float distanceUp;
    public float smooth;
    public Transform follow;

    //Smoothing and damping
    public Vector3 velocityCamSmooth = Vector3.zero;
    public float camSmoothDampTime = 0.1f;

    public bool _____________;
    public Vector3 lookDir;
    public Vector3 targetPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate() {
        Vector3 characterOffset = follow.position + new Vector3(0f, distanceUp, 0f);

        //Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0f;
        lookDir.Normalize();
        //Debug.DrawRay(this.transform.position, lookDir, Color.green);

        //Setting the target position to be the correct offset from the hovercraft
        targetPosition = characterOffset + (follow.up * distanceUp) - (lookDir * distanceAway);

        //Compensate for walls
        CompensateForWalls(characterOffset, ref targetPosition);

        //Make a smooth transition between it's current position and the position it wants to be in
        SmoothPosition(this.transform.position, targetPosition);

        //Make sure the camera is looking the right way
        transform.LookAt(follow);
    }

    private void SmoothPosition(Vector3 fromPos, Vector3 toPos) {
        //Make a smooth transition between camera's current position and the position it wants to be in
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget) {
        Debug.DrawLine(fromObject, toTarget, Color.cyan);

        //Compensate for walls between camera and target
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit)) {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
        }
    }
}
