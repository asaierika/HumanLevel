using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameEvent switchToKizuna;
    public GameEvent switchToPartner;
    public static SwitchCharacter instance; 
    public Who identity = Who.Kizuna;

    // Third option to support mini game interactables who have not been triggered
    public enum Who { Kizuna, Partner, None }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("Character switch detected");
            if (identity == Who.Kizuna && SwitchMode.instance.mode != SwitchMode.Mode.Spirit) {
                identity = Who.Partner;
                switchToPartner.TriggerEvent();
            } else {
                identity = Who.Kizuna;
                switchToKizuna.TriggerEvent();
            }
        } 
    }
}
