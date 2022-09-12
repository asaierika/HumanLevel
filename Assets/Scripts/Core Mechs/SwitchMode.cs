using System;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    public static event Action<Vector2> OnSwitchToSpirit;
    public GameEvent switchToSpirit;
    public GameObject player;
    private Mode mode = Mode.DemiHuman;
    private enum Mode { DemiHuman, Spirit }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !DialogueManager.inDialogue) {
            Debug.Log("Mode switch detected");
            if (mode == Mode.DemiHuman) {
                switchToSpirit.TriggerEvent();
                mode = Mode.Spirit;
                OnSwitchToSpirit?.Invoke(player.transform.position);
            }
        }
    }

    public void SwitchToDemiHuman() {
        if (mode == Mode.Spirit) {
            mode = Mode.DemiHuman;
        }
    }
}
