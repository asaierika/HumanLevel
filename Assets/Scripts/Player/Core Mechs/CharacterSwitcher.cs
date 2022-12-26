using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public static CharacterSwitcher instance; 
    public GameObject Kizuna;
    public GameObject Partner;
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
            SwitchCharacter();
        } 
    }

    private void SwitchCharacter() {
        if (identity == Who.Kizuna && ModeSwitcher.instance.mode != ModeSwitcher.Mode.Spirit) {
            identity = Who.Partner;
            Partner.tag = "Player";
            Kizuna.tag = "Party";
            EventManager.InvokeEvent(EventManager.Event.SWITCH_TO_PARTNER);
        } else {
            identity = Who.Kizuna;
            Partner.tag = "Party";
            Kizuna.tag = "Party";
            EventManager.InvokeEvent(EventManager.Event.SWITCH_TO_KIZUNA);
        }
    }
}
