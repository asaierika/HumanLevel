using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent response;

    void OnEnable() {
        Debug.Log(gameObject.name + " added");
        gameEvent.AddListener(this);
    }

    void OnDisable() {
        Debug.Log(gameObject.name + " added");
        gameEvent.RemoveListener(this);
    }

    public void OnEventTriggered() {
        Debug.Log("OnEventTriggered");
        response.Invoke();
    }
}
