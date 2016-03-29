using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public float jumpPower = 5f;
    public float vertMin = -60, vertMax = 60;
    public bool ____________;
    public Vector3 rot, camRot;
    Rigidbody rigid;
    Transform camTrans;
    Transform trans;
    public float mX, mY;

    public bool hasJump = true;


    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody>();
        trans = transform;
        camTrans = trans.Find("Main Camera");
    }

    // Update is called once per frame
    void FixedUpdate() {
        mX = Input.GetAxis("Mouse X");
        mY = Input.GetAxis("Mouse Y");

        rot = trans.localRotation.eulerAngles;
        rot.y += mX;

        camRot = camTrans.localRotation.eulerAngles;
        if (camRot.x > 180) camRot.x -= 360;
        if (camRot.x < -180) camRot.x += 360;
        camRot.x -= mY;
        camRot.x = Mathf.Clamp(camRot.x, vertMin, vertMax);

        trans.localRotation = Quaternion.Euler(rot);
        camTrans.localRotation = Quaternion.Euler(camRot);

        Vector3 vel = Vector3.zero;
        vel += trans.forward * Input.GetAxis("Vertical");
        vel += trans.right * Input.GetAxis("Horizontal");
        vel *= speed;
        vel.y = rigid.velocity.y;

        rigid.velocity = vel;

        if (Input.GetKey(KeyCode.Space) && hasJump) {
            hasJump = false;
            //rigid.velocity = new Vector3(rigid.velocity.x, jumpPower, rigid.velocity.z);
        }
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.tag == "Ground") {
            hasJump = true;
        }
    }
}



