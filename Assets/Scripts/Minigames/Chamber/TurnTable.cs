using System;
using System.Collections;
using UnityEngine;

// FIXME: In spirit mode, the spirit and demihuman body are blocked by game interface.
// Player will have difficulty returning to demihuman form without exiting the minigame resetting their progress.
// Could occur in other games as well.
public class TurnTable : Minigame
{
    public GameObject table;
    public LightGate lightGate;
    public float angleSpeed;
    public float acceptedAngleMargin;
    public float revertRotationSpeed;
    [SerializeField][Header("Rotation Waypoints")]
    private float[] targetAngleSeq;
    [SerializeField][Header("1 For Counter-Clockwise, Vice Versa")]
    private int[] directionSeq;
    private int stage = 0;
    private bool mistake = false;
    // Whether a waypoint have been reached
    private bool reachedWaypoint = false;
    private bool rotationComplete = false;
    [SerializeField]
    private float totalMovement = -1;
    [SerializeField]
    private float[] prefixMovementCost;

    void Awake() {
        GetTotalMovement();
    }
    
    void Update()
    {
        // In spirit mode the physical table cannot be turned.
        if (StateManager.instance.CurrPlayerState.Mode == ValidPlayerState.Form.SPIRIT
                || lightGate.IsFixed || !lightGate.IsResponsive || rotationComplete || mistake ) {
            return;
        }

        if (Math.Abs(table.transform.rotation.eulerAngles.z - targetAngleSeq[stage]) <= acceptedAngleMargin) {
            if (!reachedWaypoint) {
                Debug.Log("Dest " + stage + " reached");
                reachedWaypoint = true;
                rotationComplete = stage == targetAngleSeq.Length - 1;
                return;
            }
        }

        int rotateDir = (int) Input.GetAxisRaw("Vertical");
        if (reachedWaypoint) {
            if (rotateDir == directionSeq[stage + 1]) {
                Debug.Log("Start stage " + stage);
                // Only make player move to the next dest if they change towards the direction
                reachedWaypoint = false;
                stage++;
            } else if (Math.Abs(table.transform.rotation.eulerAngles.z - targetAngleSeq[stage]) > acceptedAngleMargin) {
                Debug.Log("Overturn");
                // If player overturns the current dest region they are in
                mistake = true;
                ResetTableAndGate();
            }
        } else {
            if (rotateDir / directionSeq[stage] == -1) {
                Debug.Log("Wrong Direction");
                mistake = true;
                ResetTableAndGate();
                return;
            } 
        }

        float angleVelocity = angleSpeed * rotateDir * Time.deltaTime;
        table.transform.rotation *= Quaternion.AngleAxis(angleVelocity, Vector3.forward);
        lightGate.UpdatePosition(GetProgress());
    }

    // NOTE: Cannot return bool hence dup code with OnKeyBoardExit()
    public bool ResetTableAndGate(bool isExit = false) {
        if (!lightGate.IsFixed) {
            StartCoroutine(LerpToStart(isExit));
            StartCoroutine(lightGate.Close());
            return true;
        }
        return false;
    }

    public override void OnKeyboardExit() {
        if (!ResetTableAndGate(true)) {
            // Immediate exit since the turntable is fixed in place.
            MinigameManager.instance.ExitMinigame();
        }
    }


    IEnumerator LerpToStart(bool isExit = false) {
        Debug.Log("Reverting");
        while (Math.Abs(table.transform.rotation.eulerAngles.z) > 2.5f) {
            // Debug.Log("Z angle " + table.transform.rotation.eulerAngles.z);
            table.transform.rotation *= Quaternion.AngleAxis(revertRotationSpeed * Time.deltaTime * Math.Sign(table.transform.rotation.eulerAngles.z - 180), Vector3.forward);
            yield return null;
        }
        
        // Turntable resets after player made a mistake
        stage = 0;
        mistake = false;
        reachedWaypoint = false;
        rotationComplete = false;
        // Buffer for smooth animation
        yield return new WaitForSeconds(0.5f);
        if (isExit) MinigameManager.instance.ExitMinigame();
    }

    private void GetTotalMovement() {
        if (directionSeq[0] == 1) {
            // Anticlockwise
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

    public override float GetProgress() {
        // Player movement from last checkpoint achieved
        if (!reachedWaypoint) {
            if (stage == 0) {
                if (directionSeq[0] == 1) {
                    return table.transform.rotation.eulerAngles.z / totalMovement;
                }
                return (360 - table.transform.rotation.eulerAngles.z) / totalMovement;
            }

            float stageMovement;

            if (directionSeq[stage] == 1) {
                if (table.transform.rotation.eulerAngles.z <= targetAngleSeq[stage - 1]) {
                    stageMovement = 360 - targetAngleSeq[stage - 1] + table.transform.rotation.eulerAngles.z;
                } else {
                    stageMovement = table.transform.rotation.eulerAngles.z - targetAngleSeq[stage - 1];
                }
            } else {
                if (table.transform.rotation.eulerAngles.z >= targetAngleSeq[stage - 1]) {
                    stageMovement = targetAngleSeq[stage - 1] + 360 - table.transform.rotation.eulerAngles.z;
                } else {
                    stageMovement = targetAngleSeq[stage - 1] - table.transform.rotation.eulerAngles.z;
                }
            }

            return (prefixMovementCost[stage - 1] + stageMovement) / totalMovement;
        } else {
            return prefixMovementCost[stage] / totalMovement;
        }
    }
}