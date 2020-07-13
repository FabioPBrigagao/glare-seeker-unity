using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public GameObject[] areas;
    public GameObject player;
    public Renderer playerRenderer;
    public Light playerLight;
    public Material lightMaterial;
    public Material darkMaterial;
    public float multiplierTimeLight;

    float intialLightRange;
    [HideInInspector] public bool isCharging;
    float colorIndex;
    int areaIndex;

    public event EventHandler OnGameplayStarted;

    const float RESTART_FALL_Y = -70;
    const float RECHARGE_MULTIPLIER = 15;

    void Awake() {
        Cursor.visible = false;
        intialLightRange = playerLight.range;
        InvokeRepeating("StartGameplay", 0f, 0.1f);
    }

    void Update() {
        PlayerFall();
        PlayerLightEmission();
        ReturnToMenu();
    }

    void PlayerFall() {
        if (player.transform.position.y < RESTART_FALL_Y) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void PlayerLightEmission() {
        if (playerLight.range > 0 && !isCharging) {
            playerLight.range -= multiplierTimeLight * Time.deltaTime;
            colorIndex += (Time.deltaTime / (intialLightRange / multiplierTimeLight));
            playerRenderer.material.Lerp(lightMaterial, darkMaterial, colorIndex);
        } else if (playerLight.range >= intialLightRange) {
            isCharging = false;
        } else if (isCharging) {
            playerLight.range += RECHARGE_MULTIPLIER * Time.deltaTime;
            colorIndex -= (Time.deltaTime / (intialLightRange / RECHARGE_MULTIPLIER));
            playerRenderer.material.Lerp(lightMaterial, darkMaterial, colorIndex);
        }
    }

    public void EnableNextArea() {
        if (areaIndex < areas.Length - 1) {
            areaIndex++;
            areas[areaIndex].SetActive(true);
        }
    }

    void ReturnToMenu(){
       if(Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void StartGameplay() {
        if (player.GetComponent<PlayerMovement>().isGrounded) {
            OnGameplayStarted?.Invoke(this, EventArgs.Empty);
            CancelInvoke();
        }
    }
}
