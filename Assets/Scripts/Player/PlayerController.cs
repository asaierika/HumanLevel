using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement bodyMovement;
    public GameObject spirit;
    // Start is called before the first frame update
    void OnEnable()
    {
        EventManager.StartListening(EventManager.Event.SWITCH_TO_SPIRIT, DisableBodyWrapper);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_DEMIHUMAN, EnableBodyWrapper);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_SPIRIT, ActivateSpiritWrapper);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_DEMIHUMAN, DeactivateSpiritWrapper);
    }

    // Update is called once per frame
    void OnDisable()
    {
        EventManager.StopListening(EventManager.Event.SWITCH_TO_SPIRIT, DisableBodyWrapper);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_DEMIHUMAN, EnableBodyWrapper);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_SPIRIT, ActivateSpiritWrapper);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_DEMIHUMAN, DeactivateSpiritWrapper);
    }

    void EnableBodyWrapper(object o = null) {
        bodyMovement.enabled = true;
    }

    void DisableBodyWrapper(object o = null) {
        bodyMovement.enabled = false;
    }

    void ActivateSpiritWrapper(object o = null) {
        spirit.SetActive(true);
    }

    void DeactivateSpiritWrapper(object o = null) {
        spirit.SetActive(false);
    }
}
