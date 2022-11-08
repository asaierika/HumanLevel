using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    // Permit a many to many relationship between events ad response
    // One event can trigger multiple response while multiple events can trigger 
    // the same response.
    public GameEvent[] gameEvents;
    public UnityEvent response;

    void OnEnable() {
        foreach (GameEvent gameEvent in gameEvents) {
            gameEvent.AddListener(this);
        }
    }

    void OnDisable() {
        foreach (GameEvent gameEvent in gameEvents) {
            gameEvent.RemoveListener(this);
        }
    }

    public void OnEventTriggered() {
        response.Invoke();
    }
}
