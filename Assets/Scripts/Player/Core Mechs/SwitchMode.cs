using System;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    public static SwitchMode instance;
    public event Action OnSwitchToSpirit;
    public GameEvent switchToSpirit;
    public GameObject player;
    private Mode mode = Mode.DemiHuman;
    private enum Mode { DemiHuman, Spirit }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SwitchCharacter.who == SwitchCharacter.Who.Kizuna && Input.GetKeyDown(KeyCode.R) 
            && !DialogueManager.inDialogue) {
            Debug.Log("Mode switch detected");
            if (mode == Mode.DemiHuman) {
                switchToSpirit.TriggerEvent();
                mode = Mode.Spirit;
                OnSwitchToSpirit?.Invoke();
            }
        }
    }

    public void SwitchToDemiHuman() {
        if (mode == Mode.Spirit) {
            mode = Mode.DemiHuman;
        }
    }
}
