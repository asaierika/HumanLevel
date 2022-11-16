using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Game Event")]
public class GameEvent : ScriptableObject
{
    private List<EventListener> listeners = new List<EventListener>();

    public void TriggerEvent() {
        Debug.Log("Event triggered");

        foreach (EventListener listener in listeners) {
            Debug.Log(listener.gameObject.name);
            listener.OnEventTriggered();
        }
    }

    public void AddListener(EventListener listener) {
        listeners.Add(listener);
    }

    public void RemoveListener(EventListener listener) {
        listeners.Remove(listener);
    }
}
