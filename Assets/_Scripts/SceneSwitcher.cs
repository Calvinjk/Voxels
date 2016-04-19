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
	    if (Input.GetKeyDown(KeyCode.Keypad0)) {
            print("Tried to switch to 0");
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            print("Tried to switch to 1");
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            print("Tried to switch to 2");
            SceneManager.LoadScene(2);
        }
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.name == "Player") {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
