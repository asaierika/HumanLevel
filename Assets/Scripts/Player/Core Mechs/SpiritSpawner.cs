using UnityEngine;

// Responsible for placing spirit at correct location when spirit mode switch occurs.
public class SpiritSpawner : MonoBehaviour
{
    public GameObject spirit;
    public GameObject body;
    public Vector2 spawnPositionOffset;

    void OnEnable() {
        Debug.Log("Spirit enabled");
        spirit.transform.localPosition = new Vector2(body.transform.localPosition.x + spawnPositionOffset.x, body.transform.localPosition.y + spawnPositionOffset.y);
    }

    // Spirit returned to body
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            SwitchMode.instance.ToggleMode();
        }
    }
}
