using UnityEngine;

// Responsible for placing spirit at correct location when spirit mode switch occurs.
public class SpiritSpawner : MonoBehaviour
{
    public GameObject subject;

    void Awake() {
        SwitchMode.instance.OnSwitchToSpirit += SpawnAt;
    }
    public void SpawnAt() {
        subject.transform.localPosition = new Vector2(0, 1f);
    }
}
