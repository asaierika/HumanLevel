using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isActive;
    public SwitchCharacter.Who identity;

    // Called when a character switch event is emitted
    public void SetPlayable() {
        if (SwitchCharacter.who == identity) {
            isActive = true;
            gameObject.GetComponent<PlayerMovement>().enabled = true;
        } else {
            isActive = false;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public bool IsActive => isActive;
}
