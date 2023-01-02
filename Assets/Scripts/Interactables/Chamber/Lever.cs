using UnityEngine;
using UnityEngine.Events;

public class Lever : Interactable
{
    // For now, the visual feedback is only the X axis inversion of handle.
    // Should be changed later
    public GameObject handle;
    private static Vector2 toggle = new Vector2(-1, 1);
    // Assumes starting position is not in target position.
    public bool isInTargetPosition = false;
    public UnityEvent OnTargetPositionReached;
    public UnityEvent OnRevertToOriginalPosition;
    
    public override void Interact() {
        // if (currPosition == Position.LEFT) {
        //     handle.transform.localScale = INVERT;
        //     currPosition = Position.RIGHT;
        // } else {
        //     handle.transform.localScale = REVERT;
        //     currPosition = Position.LEFT;
        // }
        handle.transform.localScale *= toggle;

        isInTargetPosition = !isInTargetPosition;
        if (isInTargetPosition) {
            OnTargetPositionReached?.Invoke();
        } else {
            OnRevertToOriginalPosition?.Invoke();
        }
    }
}
