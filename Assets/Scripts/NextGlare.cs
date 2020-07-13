using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextGlare : MonoBehaviour {
    SceneController scene;

    void Awake() {
        scene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
    }

    void OnTriggerEnter(Collider other) {
        if(gameObject.tag == "Final Glare"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        scene.isCharging = true;
        scene.EnableNextArea();
        gameObject.SetActive(false);
    }

}
