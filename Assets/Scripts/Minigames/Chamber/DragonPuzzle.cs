using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPuzzle : Minigame
{
    public override float GetProgress() {
        return 0;
    }

    public override void OnKeyboardExit()
    {
        throw new System.NotImplementedException();
    }
}
