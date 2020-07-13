using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookCameraOverride : MonoBehaviour {
    public CinemachineFreeLook Cam;
    public GameObject player;
    public SceneController scene;

    float maxSpeedY;
    float maxSpeedX;

    void Awake() {
        scene.OnGameplayStarted += FollowPlayer;

        Cam.LookAt = player.transform;
        maxSpeedY = Cam.m_YAxis.m_MaxSpeed;
        maxSpeedX = Cam.m_XAxis.m_MaxSpeed;
    }

    void Update() => RotateCamera();

    void RotateCamera() {
        if (Input.GetMouseButton(1)) {
            Cam.m_YAxis.m_InputAxisName = "Mouse Y";
            Cam.m_XAxis.m_InputAxisName = "Mouse X";
            Cam.m_YAxis.m_MaxSpeed = maxSpeedY;
            Cam.m_XAxis.m_MaxSpeed = maxSpeedX;
        } else {
            Cam.m_YAxis.m_InputAxisName = "";
            Cam.m_XAxis.m_InputAxisName = "";
            Cam.m_YAxis.m_MaxSpeed = 0;
            Cam.m_XAxis.m_MaxSpeed = 0;
        }
    }

    void FollowPlayer(object sender, EventArgs e) {
        Cam.Follow = player.transform;
    }

}
