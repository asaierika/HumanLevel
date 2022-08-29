using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSwitch : MonoBehaviour
{
    public void SwitchState() {
        Debug.Log("Change gameObject active state");
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
