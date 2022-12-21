using System;
using UnityEngine;

// TODO: For SwitchMode and SwitchCharacter core events can reference @repo unity_mobile_app @class EventManager for reference.
// TODO: Tag spirit objects and SetActive() via tags instead not manual assignment
public class SwitchMode : MonoBehaviour
{
    public static SwitchMode instance;
    public GameObject spiritFilter;
    public Mode mode = Mode.DemiHuman;
    public enum Mode { DemiHuman, Spirit }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SwitchCharacter.instance.identity == SwitchCharacter.Who.Kizuna && Input.GetKeyDown(KeyCode.R) 
            && !DialogueManager.instance.inDialogue) {
            Debug.Log("Mode switch detected");
            if (mode == Mode.DemiHuman) {
                EventManager.InvokeEvent(EventManager.Event.SWITCH_TO_SPIRIT);
                mode = Mode.Spirit;
            }
        }
    }

    public void ToggleMode() {
        if (mode == Mode.DemiHuman) {
            EventManager.InvokeEvent(EventManager.Event.SWITCH_TO_SPIRIT);
            mode = Mode.Spirit;
            spiritFilter.SetActive(true);
        } else if (mode == Mode.Spirit) {
            EventManager.InvokeEvent(EventManager.Event.SWITCH_TO_DEMIHUMAN);
            mode = Mode.DemiHuman;
            spiritFilter.SetActive(false);
        } else {
            Debug.LogError("Invalid player mode");
        }
    }
}
