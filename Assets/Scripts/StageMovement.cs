using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour {

    GameObject player;

    public Transform[] nodes;
    public float cycleTime;

    Vector3 position;
    Vector3 start;
    Vector3 end;
    int nodeIndex = 0;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        start = nodes[nodeIndex].position;
        end = nodes[nodeIndex + 1].position;
    }

    void FixedUpdate() {

        position = Vector3.Lerp(start, end, Mathf.Cos(Time.time / cycleTime * Mathf.PI * 2) * -.5f + .5f);
        gameObject.GetComponent<Rigidbody>().MovePosition(position);

        // WORK IN PROGRESS
        //if (position == end) {
        //    if (nodeIndex == nodes.Length - 2) {
        //        nodeIndex = 0;
        //        start = nodes[nodeIndex].position;
        //    } else  {
        //        nodeIndex += 1;
        //        start = nodes[nodeIndex + 1].position;
        //    }
        //}
        //if (position == start) {
        //    if (nodeIndex == nodes.Length - 2) {
        //        nodeIndex = 0;
        //        end = nodes[nodeIndex].position;
        //        nodeIndex = -1;
        //    } else {
        //        nodeIndex += 1;
        //        end = nodes[nodeIndex + 1].position;
        //    }
        //}
    }

    void OnTriggerStay(Collider col) {
        if (col.gameObject == player) {
           player.GetComponent<CharacterController>().Move(gameObject.GetComponent<Rigidbody>().velocity * Time.deltaTime);
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
