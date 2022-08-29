using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameEvent gameEvent;

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D body)
    {
        if (body.gameObject.tag == "Player") {
            gameEvent.TriggerEvent();
        }
    }
}
