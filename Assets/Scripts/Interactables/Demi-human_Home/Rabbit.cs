using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Interactable
{
    public Conversation convo1, convo2;

    public static bool talked;

     void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        if (!talked)
        {
            DialogueManager.StartConversation(convo1);
            talked = true;
            return;
        }

        DialogueManager.StartConversation(convo2);
    }
}
