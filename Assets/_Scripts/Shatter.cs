﻿using UnityEngine;
using System.Collections;

public class Shatter : MonoBehaviour {

    public GameObject voxel;
    public int shatterMult = 1;     //1 = 8 pieces, 2 = 64 pieces, etc.   Pieces = 8^shatterMult.  Does nothing if shatterMult < 1
    public float voxelLifeSpan = 0f;

    public bool __SWITCHES__;
    public bool enterToShatter = false;
    public bool ____________;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (enterToShatter && Input.GetKeyDown(KeyCode.Return)) {
            Die();
            //Time.timeScale = 0;
        }
	}

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.name == "Player" || coll.gameObject.name == "Voxel") {  
            Die();
        }
    }
    public void Die() { //Basic voxel shattering of cubes or rectangles
        if (shatterMult > 0) { //shatterMult must be a positive integer
            Vector3 pos = transform.position;
            Vector3 scale = transform.localScale;
            Quaternion rot = transform.rotation;
            Material mat = GetComponent<Renderer>().material;

            Destroy(gameObject);

            //Voxel shatter logic
            float boundaryX = ((Mathf.Pow(2, shatterMult) - 1) / Mathf.Pow(2, shatterMult + 1)) * scale.x;
            float boundaryY = ((Mathf.Pow(2, shatterMult) - 1) / Mathf.Pow(2, shatterMult + 1)) * scale.y;
            float boundaryZ = ((Mathf.Pow(2, shatterMult) - 1) / Mathf.Pow(2, shatterMult + 1)) * scale.z;

            float stepX = scale.x / Mathf.Pow(2, shatterMult);
            float stepY = scale.y / Mathf.Pow(2, shatterMult);
            float stepZ = scale.z / Mathf.Pow(2, shatterMult);

            for (float x = -boundaryX; x <= boundaryX; x += stepX) {
                for (float y = -boundaryY; y <= boundaryY; y += stepY) {
                    for (float z = -boundaryZ; z <= boundaryZ; z += stepZ) {
                        Vector3 adjustmentVec = RotateAll(new Vector3(x, y, z), rot.eulerAngles);
                        CreateVoxel(pos, adjustmentVec, rot, scale, mat);
                    }
                }
            }
        }
    }

    Vector3 RotateAll(Vector3 v, Vector3 angles) {
        return RotateAroundAxis(RotateAroundAxis(RotateAroundAxis(v, angles.z, new Vector3(0f, 0f, 1f)), 
                                                angles.x, new Vector3(1f, 0f, 0f)), 
                                                angles.y, new Vector3(0f, 1f, 0f));
    }

    Vector3 RotateAroundAxis(Vector3 vector, float angle, Vector3 axis) {
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        return q * vector;
    }

    void CreateVoxel(Vector3 pos, Vector3 adj, Quaternion rot, Vector3 scale, Material mat) {
        GameObject curVox = Instantiate(voxel, pos + adj, rot) as GameObject;
        curVox.transform.localScale = scale / Mathf.Pow(2, shatterMult);
        curVox.GetComponent<Renderer>().material = mat;
        curVox.name = "Voxel";

        KillSelf killSelf = (KillSelf)curVox.GetComponent(typeof(KillSelf));
        killSelf.timeAlive = voxelLifeSpan;
    }
}
