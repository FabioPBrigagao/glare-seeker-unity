using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour {

    GameObject player;

    public Transform[] nodes;
    public float cycleTime;
    public Rigidbody rb;

    Vector3 position;
    Vector3 start;
    Vector3 end;
    int nodeIndex = 0;

    void Awake() {
        start = nodes[nodeIndex].position;
        end = nodes[nodeIndex + 1].position;
    }

    void FixedUpdate() {
        position = Vector3.Lerp(start, end, Mathf.Cos(Time.time / cycleTime * Mathf.PI * 2) * -.5f + .5f);
        rb.MovePosition(position);
    }

    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "Player") {
           col.GetComponent<CharacterController>().Move(rb.velocity * Time.deltaTime);
        }
    }

    private void OnDrawGizmos() {
        if (nodes != null && nodes.Length != 0) {
            for (int i = 0; i < nodes.Length - 1; i++) {
                Gizmos.DrawLine(nodes[i].position, nodes[i + 1].position);
            }
            Gizmos.DrawLine(nodes[nodes.Length - 1].position, nodes[0].position);
        }
    }

}
