using UnityEngine;
using System.Collections;

public class Shatter : MonoBehaviour {

    public GameObject voxel;
    public int shatterMult          = 1;     //1 = 8 pieces, 2 = 64 pieces, etc.   Pieces = 8^shatterMult.  Does nothing if shatterMult < 1
    public float shatterSpeed       = 1f;
    public float minVoxelLifeSpan   = 0f;
    public float maxVoxelLifeSpan   = 0f;

    public bool blowApart           = false;
    public bool keepVelocity        = false;
    public bool shatterOnVoxelTouch = false;
    public bool enterToShatter      = false;
    public bool wasEnvironment      = false;
    public bool ____________;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (enterToShatter && Input.GetKeyDown(KeyCode.Return)) {
            Die();
            //Time.timeScale = 0;
        }
	}

    void OnCollisionEnter(Collision coll) {
        if (shatterOnVoxelTouch && coll.gameObject.name == "Swarmer Voxel") {  
            Die();
        }
    }
    public void Die() { //Basic voxel shattering of cubes or rectangles
        if (wasEnvironment) {
            Transform carver = transform.GetChild(0);
            carver.GetComponent<NavMeshObstacle>().enabled = true;
            carver.position = new Vector3(carver.position.x, carver.position.y + 1, carver.position.z);
            carver.parent = null;
        }

        if (shatterMult > 0) { //shatterMult must be a positive integer
            Vector3 pos = transform.position;
            Vector3 scale = transform.localScale;
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Quaternion rot = transform.rotation;
            Material mat = GetComponent<Renderer>().material;
            string name = gameObject.name;

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
                        CreateVoxel(pos, adjustmentVec, rot, scale, mat, name, velocity);
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

    void CreateVoxel(Vector3 pos, Vector3 adj, Quaternion rot, Vector3 scale, Material mat, string parentName, Vector3 velocity) {
        GameObject curVox = Instantiate(voxel, pos + adj, rot) as GameObject;
        curVox.transform.localScale = scale / Mathf.Pow(2, shatterMult);
        curVox.GetComponent<Renderer>().material = mat;
        curVox.name = parentName + " Voxel";

        KillSelf killSelf = (KillSelf)curVox.GetComponent(typeof(KillSelf));
        killSelf.minTimeAlive = minVoxelLifeSpan;
        killSelf.maxTimeAlive = maxVoxelLifeSpan;
        killSelf.startTimer = true;

        AddVoxelVelocity(pos, curVox, velocity);

    }

    void AddVoxelVelocity(Vector3 center, GameObject voxel, Vector3 startingVelocity) {
        if (blowApart) {
            Vector3 vectorFromCenter = voxel.transform.position - center;
            voxel.GetComponent<Rigidbody>().AddForce(vectorFromCenter.normalized * shatterSpeed);
        }
        
        if (keepVelocity) {
            voxel.GetComponent<Rigidbody>().velocity += startingVelocity;
        }
    }
}
