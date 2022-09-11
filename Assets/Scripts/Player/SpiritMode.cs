using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMode : MonoBehaviour
{
    private bool isSpiritMode;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isSpiritMode)
            {
           
            StartCoroutine(Enter());
            }
            else
            {
            
            StartCoroutine(Exit());
            }
        }
    }

    IEnumerator Enter() 
    {
        yield return new WaitForSeconds(0.1f);
        GameEvents.instance.EnterSpiritMode();
        isSpiritMode = true;
    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.1f);
        GameEvents.instance.ExitSpiritMode();
        isSpiritMode = false;
    }
}
