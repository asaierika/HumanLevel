using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameEvent switchToKizuna;
    public GameEvent switchToPartner;
    public static Who who = Who.Kizuna;

    // Third option to support mini game interactables who have not been triggered
    public enum Who { Kizuna, Partner, None }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("Character switch detected");
            if (who == Who.Kizuna) {
                switchToPartner.TriggerEvent();
                who = Who.Partner;
            } else {
                switchToKizuna.TriggerEvent();
                who = Who.Kizuna;
            }
        } 
    }
}
