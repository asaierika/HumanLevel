using UnityEngine;

public class Mode : MonoBehaviour
{
    public bool isActiveInSpirit;

    public void SwitchToSpiritState() {
        gameObject.SetActive(isActiveInSpirit);
    }

    public void SwitchToDemiHumanState() {
        gameObject.SetActive(!isActiveInSpirit);
    }
}
