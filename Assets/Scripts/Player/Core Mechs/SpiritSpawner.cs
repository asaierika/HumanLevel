using UnityEngine;

// Responsible for placing spirit at correct location when spirit mode switch occurs.
public class SpiritSpawner : MonoBehaviour
{
    public GameObject spirit;
    public GameObject body;
    public Vector2 spawnPositionOffset;
    public GameEvent switchToDemi;

    void OnEnable() {
        Debug.Log("Spirit enabled");
        SwitchMode.instance.OnSwitchToSpirit += SpawnSpirit;
    }
    
    public void SpawnSpirit() {
        Debug.Log(spirit.name);
        spirit.transform.localPosition = new Vector2(body.transform.localPosition.x + spawnPositionOffset.x, body.transform.localPosition.y + spawnPositionOffset.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switchToDemi.TriggerEvent();
            SwitchMode.instance.mode = SwitchMode.Mode.DemiHuman;
        }
    }

    void OnDisable() {
        Debug.Log("Spirit disabled");
        SwitchMode.instance.OnSwitchToSpirit -= SpawnSpirit;
    }
}
