using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Remove the class
public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
