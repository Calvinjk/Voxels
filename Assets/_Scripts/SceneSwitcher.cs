using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    private int sceneToLoad = -1;

    // Use this for initialization
    void Start () {
        if (gameObject.tag == "Lava") {
            sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        }
        else { //Endtile
            sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.name == "Player") {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
