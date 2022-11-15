using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for placing spirit at correct location when spirit mode switch occurs.
public class SpiritSpawner : MonoBehaviour
{
    public GameObject subject;

    void Awake() {
        SwitchMode.OnSwitchToSpirit += SpawnAt;
    }
    public void SpawnAt(Vector2 location) {
        
        subject.transform.position = new Vector2(location.x, location.y + 0.2f);
    }
}
