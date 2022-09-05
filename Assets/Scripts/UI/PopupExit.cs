using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupExit : Interactable
{
    public bool hasPopup = false;

    public GameEvent exit;
    // Update is called once per frame
    void Update()
    {
        if (hasPopup && Input.GetKeyDown(KeyCode.Escape)) {
            exit.TriggerEvent();
        }
    }

    public void Popup() {
        hasPopup = true;
    }
}
