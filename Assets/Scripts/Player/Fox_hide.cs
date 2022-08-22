using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_hide : MonoBehaviour
{
    public GameObject fox;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!fox.activeInHierarchy)
            {
                fox.SetActive(true);
            }
        }
    }
}
