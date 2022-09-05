using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSwitch : MonoBehaviour
{
    public bool isActiveInSpirit;

    public void SwitchToSpiritState() {
        gameObject.SetActive(isActiveInSpirit);
    }

    public void SwitchToDemiHumanState() {
        gameObject.SetActive(!isActiveInSpirit);
    }

    public void GenericSwitchState() {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
