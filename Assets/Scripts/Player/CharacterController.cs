using UnityEngine;
using System.Collections.Generic;

// FIXME: Partner cannot move when SwitchToKizuna then SwitchToSpirit then SwitchToDemi and back to Partner,
// because the two Kizuna Demi switches will result in excessive locks.
public class CharacterController : MonoBehaviour
{
    public CharacterMovement movement;

    protected void FreezeMovementWrapper(object o = null) {
        movement.Freeze();
    }

    protected void TryRestoreMovementWrapper(object o = null) {
        movement.TryRestore();
    }
}
