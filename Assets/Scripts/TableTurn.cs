using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTurn : MonoBehaviour
{
    public float angleSpeed;
    public float acceptedAngleMargin;
    public float[] targetAngleSeq;
    public int[] directionSeq;
    public int stage = 0;
    public bool mistake = false;
    public bool reached = false;
    public bool complete = false;
    
    void Update()
    {
        if (!complete) {
            if (!mistake) {
                if (transform.rotation.eulerAngles.z <= targetAngleSeq[stage] + acceptedAngleMargin && transform.rotation.eulerAngles.z >= targetAngleSeq[stage] - acceptedAngleMargin) {
                    if (!reached) {
                        Debug.Log("Dest " + stage + " reached");
                        stage++;
                        reached = true;
                        if (stage == targetAngleSeq.Length) {
                            complete = true;
                            return;
                        }
                    }
                }

                int rotateDir = (int) Input.GetAxisRaw("Vertical");
                if (reached) {
                    if (rotateDir == directionSeq[stage]) {
                        // Only make player move to the next dest if they change towards the direction
                        reached = false;
                        Debug.Log("Start stage " + stage);
                    } else if (transform.rotation.eulerAngles.z > targetAngleSeq[stage - 1] + acceptedAngleMargin || transform.rotation.eulerAngles.z < targetAngleSeq[stage - 1] - acceptedAngleMargin) {
                        // If player overturns the current dest region they are in
                        mistake = true;
                        Debug.Log("Overturn");
                        StartCoroutine(returnToStart());
                    }
                } else {
                    if (rotateDir / directionSeq[stage] == -1) {
                        Debug.Log("Wrong Direction");
                        mistake = true;
                        StartCoroutine(returnToStart());
                    } 
                }

                float angleVelocity = angleSpeed * rotateDir;
                transform.Rotate(new Vector3(0, 0, angleVelocity));
            }
        }
    }

    IEnumerator returnToStart() {
        // 
        yield return null;
    }
}