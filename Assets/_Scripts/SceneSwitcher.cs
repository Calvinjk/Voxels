using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    public bool introScene = false;
    public GameObject titleCanvas;
    public GameObject controlCanvas;
    private int sceneToLoad = -1;

    // Use this for initialization
    void Start () {
        if (gameObject.tag == "Lava") {
            sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        }
        else { //Endtile
            sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }

        if (sceneToLoad == (SceneManager.sceneCountInBuildSettings)) { //After final level, go back to start
            sceneToLoad = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(introScene || SceneManager.GetActiveScene().buildIndex == 1) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (Input.GetKeyDown(KeyCode.Return) && introScene) {
                if(controlCanvas.activeInHierarchy) {
                    titleCanvas.SetActive(true);
                    controlCanvas.SetActive(false);
                } else {
                    titleCanvas.SetActive(false);
                    controlCanvas.SetActive(true);
                }
            }
        }

        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.name == "Player") {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
