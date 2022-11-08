using UnityEngine;

public class Lever : Interactable
{
    // For now, the visual feedback is only the X axis inversion of handle.
    // Should be changed later
    public GameObject handle;
    private static Vector2 INVERT = new Vector2(-1, 1);
    private static Vector2 REVERT = new Vector2(1, 1);

    public Position currPosition;
    public bool isInTargetPosition;
    public enum Position { LEFT, RIGHT };
    public PairEvent eventPair;


    void Update() {
        TryInteract();
    }
    
    public override void Interact() {
        if (currPosition == Position.LEFT) {
            handle.transform.localScale = INVERT;
            currPosition = Position.RIGHT;
        } else {
            handle.transform.localScale = REVERT;
            currPosition = Position.LEFT;
        }

        isInTargetPosition = !isInTargetPosition;
        if (isInTargetPosition) {
            eventPair.TriggerPositive();
        } else {
            eventPair.TriggerNegative();
        }
    }
}
