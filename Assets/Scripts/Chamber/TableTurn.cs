using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInterfaces;

public class TableTurn : MonoBehaviour, ICompletable
{
    public float angleSpeed;
    public float acceptedAngleMargin;
    public float[] targetAngleSeq;
    public int[] directionSeq;
    public int stage = 0;
    public bool mistake = false;
    // Whether a waypoint have been reached
    public bool reached = false;
    public bool complete = false;
    public float revertRotationSpeed;
    public GameEvent revertToStart;
    public GameEvent gameComplete;
    public GameEvent closePopup;
    private float totalMovement = -1;
    public float[] prefixMovementCost;
    public Lockable gateLock;

    // The game this mechanism belongs to.
    public MiniGame game;
    
    void Update()
    {
        if (game.isCorrectCharacter) {
            if (complete && gateLock.IsLocked) {
                // When the lock to fix the gate at open position is engaged
                gameComplete.TriggerEvent();
            } else if (!gateLock.IsLocked && !complete) {
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
                            StartRevert(false);
                            Debug.Log("Overturn");
                        }
                    } else {
                        if (rotateDir / directionSeq[stage] == -1) {
                            Debug.Log("Wrong Direction");
                            mistake = true;
                            StartRevert(false);
                        } 
                    }

                    float angleVelocity = angleSpeed * rotateDir;
                    transform.Rotate(new Vector3(0, 0, angleVelocity));
                }
            }
        }
    }

    public void StartRevert(bool isExit) {
        if (!gateLock.IsLocked) {
            revertToStart.TriggerEvent();
            StartCoroutine(returnToStart(isExit));
        }
    }

    IEnumerator returnToStart(bool isExit) {
        Debug.Log("Reverting");
        while (Mathf.Abs(transform.rotation.eulerAngles.z) > 0.5f) {
            Debug.Log("Z angle " + transform.rotation.eulerAngles.z);
            transform.Rotate(new Vector3(0, 0, -revertRotationSpeed * transform.rotation.eulerAngles.z * Time.deltaTime));
            yield return null;
        }

        Debug.Log("Revert complete");
        Debug.Log("Z angle " + transform.rotation.eulerAngles.z);
        // Turntable complete reset after player made a mistake
        stage = 0;
        mistake = false;
        reached = false;
        complete = false;
        if (isExit) {
            closePopup.TriggerEvent();
        }
    }

    public float GetCompletionStatus() {
        // Computed only once
        if (totalMovement == -1) {
            if (directionSeq[0] == 1) {
                // Counterclokwise
                totalMovement = targetAngleSeq[0];
            } else {
                // Clockwise
                totalMovement = 360 - targetAngleSeq[0];
            }

            prefixMovementCost[0] = totalMovement;

            for (int i = 1; i < targetAngleSeq.Length ; i++) {
                // Assume no movement exceeed 360 degrees
                if (directionSeq[i] == 1) {
                    if (targetAngleSeq[i] <= targetAngleSeq[i - 1]) {
                        totalMovement += 360 - targetAngleSeq[i - 1] + targetAngleSeq[i];
                    } else {
                        totalMovement += targetAngleSeq[i] - targetAngleSeq[i - 1];
                    }
                } else {
                    if (targetAngleSeq[i] >= targetAngleSeq[i - 1]) {
                        totalMovement += targetAngleSeq[i - 1] + 360 - targetAngleSeq[i];
                    } else {
                        totalMovement += targetAngleSeq[i - 1] - targetAngleSeq[i];
                    }          
                }
                prefixMovementCost[i] = totalMovement;
            }
        }

        float stageMovement;
        // Player movement from last checkpoint achieved
        if (stage == 0) {
            if (directionSeq[0] == 1) {
                return transform.rotation.eulerAngles.z / totalMovement;
            } else {
                return (360 - transform.rotation.eulerAngles.z) / totalMovement;
            }
        }

        if (directionSeq[stage] == 1) {
            // Can be written as such as the waypoints must alternate in direction
            if ((transform.rotation.eulerAngles.z < targetAngleSeq[stage - 1] && transform.rotation.eulerAngles.z > targetAngleSeq[stage - 1] - acceptedAngleMargin)
                    || (targetAngleSeq[stage - 1] < acceptedAngleMargin && transform.rotation.eulerAngles.z > 360 - acceptedAngleMargin + targetAngleSeq[stage - 1])) {
                // Player moving beyond the position of last waypoint but still within acceptable margin
                stageMovement = 0;
            } else {
                if (transform.rotation.eulerAngles.z <= targetAngleSeq[stage - 1]) {
                    stageMovement = 360 - targetAngleSeq[stage - 1] + transform.rotation.eulerAngles.z;
                } else {
                    stageMovement = transform.rotation.eulerAngles.z - targetAngleSeq[stage - 1];
                }
            }
        } else {
            // Can be written as such as the waypoints must alternate in direction
            if ((transform.rotation.eulerAngles.z > targetAngleSeq[stage - 1] && transform.rotation.eulerAngles.z < targetAngleSeq[stage - 1] + acceptedAngleMargin) 
                    || (targetAngleSeq[stage - 1] > 360 - acceptedAngleMargin && transform.rotation.eulerAngles.z < acceptedAngleMargin - 360 + targetAngleSeq[stage - 1])) {
                // Player moving beyond the position of last waypoint but still within acceptable margin (Clockwise)
                stageMovement = 0;
            } else {
                if (transform.rotation.eulerAngles.z >= targetAngleSeq[stage - 1]) {
                    stageMovement = targetAngleSeq[stage - 1] + 360 - transform.rotation.eulerAngles.z;
                } else {
                    stageMovement = targetAngleSeq[stage - 1] - transform.rotation.eulerAngles.z;
                }    
            }      
        }

        return (prefixMovementCost[stage - 1] + stageMovement) / totalMovement;
    }
}