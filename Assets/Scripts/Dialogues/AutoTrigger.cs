using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DemiHumanDialogueManager>().StartDialogue(dialogue);
    }
}
