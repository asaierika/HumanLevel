using System;
using UnityEngine;

// TODO: For SwitchMode and SwitchCharacter core events can reference @repo unity_mobile_app @class EventManager for reference.
public class SwitchMode : MonoBehaviour
{
    public static SwitchMode instance;
    public Action OnSwitchToSpirit;
    public GameEvent switchToSpirit;
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
                switchToSpirit.TriggerEvent();
                mode = Mode.Spirit;
                OnSwitchToSpirit?.Invoke();
            }
        }
    }
}
