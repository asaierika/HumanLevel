using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour 
{
    public Vector2[] route = new Vector2[2];
    public TableTurn table;
    public float closeSpeed;
    public float closeTightness;
    public float sqrCloseTightness;
    // Level completed the door will stay open
    public bool isOpen = false;
    public bool tableEngaged= false;

    void Start() {
        route[0] = new Vector2(transform.localPosition.x, transform.localPosition.y);
        sqrCloseTightness = closeTightness * closeTightness;
    }

    void Update() {
        if (tableEngaged && !isOpen) {
            transform.localPosition = (route[1] - route[0]) * table.GetCompletionStatus() + route[0];
        }
    }

    public void ActivateClose() {
        StartCoroutine(Close());
    }

    IEnumerator Close() {
        while (Vector3.SqrMagnitude((Vector2) transform.localPosition - route[0]) > sqrCloseTightness) {
            transform.localPosition += Vector3.Normalize(route[0] - route[1]) * closeSpeed * Time.deltaTime;
            yield return null;
        }
    }

    public void FixStatus() {
        isOpen = true;
    }

    public void Activate() {
        tableEngaged = true;
    }

    public void Deactivate() {
        tableEngaged = false;
    }
}