using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isActive;
    public SwitchCharacter.Who identity;
    public GameObject character;

    // Called when a character switch event is emitted
    public void SetPlayable() {
        // FIXME: Logic unclear, temporary fix only to prevent body and spirit being able to move simultaneously when switch character is trigger in spirit mode
        if (SwitchCharacter.instance.identity == identity && SwitchMode.instance.mode != SwitchMode.Mode.Spirit) {
            isActive = true;
            character.GetComponent<PlayerMovement>().enabled = true;
        } else {
            isActive = false;
            character.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public bool IsActive => isActive;
}
