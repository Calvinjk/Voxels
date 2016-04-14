using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public enum CamStates {
        Follow,
        Target
    }

    public float distanceAway;
    public float distanceUp;
    public float smooth;
    public float wallOffset;
    public Transform follow;

    //Smoothing and damping
    private Vector3 velocityCamSmooth = Vector3.zero;
    public float camSmoothDampTime = 0.1f;
    private Vector3 velocityLookDir = Vector3.zero;
    public float lookDirDampTime = 0.1f;

    private Vector3 lookDir;
    private Vector3 curLookDir;
    private Vector3 targetPosition;
    private CamStates camState = CamStates.Follow;

	// Use this for initialization
	void Start () {
        lookDir = curLookDir = follow.forward;
	}
	
	// Update is called once per frame
    void LateUpdate() {
        //Update states!
        if (Input.GetKey(KeyCode.Mouse0)) {  //TODO - what button should this be?   Should it be a toggle instead?
            camState = CamStates.Target;
        } else {
            camState = CamStates.Follow;
        }

        //Get values
        float leftX = Input.GetAxis("Horizontal");
        float leftY = Input.GetAxis("Vertical");

        Vector3 characterOffset = follow.position + new Vector3(0f, distanceUp, 0f);

        //Camera state machine!
        switch(camState) {
            case CamStates.Follow:
                lookDir = Vector3.Lerp(follow.right * (leftX < 0 ? 1f : -1f), follow.forward * (leftY < 0 ? -1f : 1f), Mathf.Abs(Vector3.Dot(this.transform.forward, follow.forward)));

                //Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
                curLookDir = Vector3.Normalize(characterOffset - this.transform.position);
                curLookDir.y = 0.0f;

                //Damping makes it so we do not update targetPosition while pivoting: camera shouldn't rotate around player
                curLookDir = Vector3.SmoothDamp(curLookDir, lookDir, ref velocityLookDir, lookDirDampTime);

                targetPosition = characterOffset + (follow.up * distanceUp) - (Vector3.Normalize(curLookDir) * distanceAway);
                break;
            case CamStates.Target:
                lookDir = follow.forward;

                //Setting the target position to be the correct offset from the player
                targetPosition = characterOffset + (follow.up * distanceUp) - (lookDir * distanceAway);
                break;
        }

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
        //Compensate for walls between camera and target
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit)) {
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);  //This will put the camera "inside" the wall in certain situations

            //Moves the camera's target a set position away from walls
            Vector3 offset = follow.position - wallHit.point;
            offset.Normalize();
            offset *= wallOffset;

            toTarget += offset;
        }
    }
}
