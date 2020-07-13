using UnityEngine;

public class LockNodes : MonoBehaviour {
    private Vector3 startPos;

    void Awake() {
        startPos = transform.position;
    }

    void Update() {
        transform.position = startPos;
    }
}