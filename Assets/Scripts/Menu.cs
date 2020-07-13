using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour{


    public GameObject title;
    public GameObject start;
    public GameObject menuButton;
    public GameObject controlsButton;
    public GameObject controlsExplain;

    private void Awake() {
        Cursor.visible = true;
    }

    public void ControlsButton(){
        title.SetActive(false);
        start.SetActive(false);
        controlsButton.SetActive(false);
        menuButton.SetActive(true);
        controlsExplain.SetActive(true);
    }

    public void MenuButton(){
        title.SetActive(true);
        start.SetActive(true);
        controlsButton.SetActive(true);
        menuButton.SetActive(false);
        controlsExplain.SetActive(false);
    }

    public void StartGameButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitButton(){
        Application.Quit();
    }

}
