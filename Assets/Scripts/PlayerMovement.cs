using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    SceneController scene;

    public CharacterController controller;
    public Transform groundCheckPos;
    public LayerMask groundMask;

    public float speed;
    public float jumpPower;
    public float jumpTime;
    public float rotationTime;

    float x;
    float z;
    float rotationVelocity;
    float jumpTimeCount = -1;
    public bool startGameplay = false;
    bool isJumping;
    public bool isGrounded;
    Vector3 directionXZ;
    Vector3 directionY;

    const float GROUND_CHECK_RADIUS = 0.1f;
    const float GRAVITY = -30f;

    void Awake() {
        scene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        scene.OnGameplayStarted += StartGame;
    }

    void FixedUpdate() {
        PlayerMotion();
        Gravity();
    }

    void Update() {
        if (startGameplay) UserInput();
        CheckIfGrounded();
    }

    void UserInput() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        isJumping = Input.GetMouseButton(0);
    }

    void PlayerMotion() {
        // Movement & Rotation
        directionXZ = new Vector3(x, 0f, z).normalized;
        if (directionXZ.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(directionXZ.x, directionXZ.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        // Hold Jump
        if (isJumping && isGrounded && jumpTimeCount == 0) {
            directionY.y = jumpPower;
            jumpTimeCount = jumpTime;
        } else if (isJumping && jumpTimeCount > 0) {
            directionY.y = jumpPower;
            jumpTimeCount -= Time.deltaTime;
        } else if (!isJumping) {
            jumpTimeCount = 0;
        }
        controller.Move(directionY * Time.deltaTime);
    }

    void Gravity() {
        if (!isGrounded) directionY.y += GRAVITY * Time.deltaTime;
        else directionY.y = -2f;
        controller.Move(directionY * Time.deltaTime);
    }

    void CheckIfGrounded() {
        isGrounded = Physics.CheckSphere(groundCheckPos.position, GROUND_CHECK_RADIUS, groundMask);
    }

    void StartGame(object sender, EventArgs e) {
        startGameplay = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckPos.position, GROUND_CHECK_RADIUS);
    }
}
