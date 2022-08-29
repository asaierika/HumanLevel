using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public LevelLoader loader;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            loader.LoadNextLevel("Ryu's Shop");
        }
    }
}
