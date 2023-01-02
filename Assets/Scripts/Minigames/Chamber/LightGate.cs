using System.Collections;
using UnityEngine;

public class LightGate : MonoBehaviour 
{
    public Pair<Vector2, Vector2> rightDoorPositions;
    public Pair<Vector2, Vector2> leftDoorPositions;
    public GameObject rightDoor;
    public GameObject leftDoor;
    public Lockable gateLock;
    public float closeSpeed;
    public float closeTightness;
    private float sqrCloseTightness;
    // Level completed the door will stay open
    public bool isOpen = false;
    // When in animation, the door is not responsive to associated object status e.g. turntable
    private bool isResponsive = true;

    void Start() {
        sqrCloseTightness = closeTightness * closeTightness;
    }

    public void UpdatePosition(float progress) {
        rightDoor.transform.localPosition = (rightDoorPositions.tail - rightDoorPositions.head) * progress + rightDoorPositions.head;
        leftDoor.transform.localPosition = (leftDoorPositions.tail - leftDoorPositions.head) * progress + leftDoorPositions.head;
    }

    public IEnumerator Close() {
        isResponsive = false;
        while (Vector3.SqrMagnitude(rightDoor.transform.localPosition - leftDoor.transform.localPosition) > sqrCloseTightness) {
            rightDoor.transform.localPosition += Vector3.Normalize(rightDoorPositions.head - rightDoorPositions.tail) * closeSpeed * Time.deltaTime;
            leftDoor.transform.localPosition += Vector3.Normalize(leftDoorPositions.head - leftDoorPositions.tail) * closeSpeed * Time.deltaTime;
            yield return null;
        }
        isResponsive = true;
    }

    public bool IsFixed => gateLock.IsLocked;
    public bool IsResponsive => isResponsive;

    public void OpenComplete() {
        isOpen = true;
    }
}