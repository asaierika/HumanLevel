using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private static bool isPlayed;
    public GameObject timeline;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayed)
        {
        timeline.SetActive(true);
        isPlayed = true;
        }
    }
}
