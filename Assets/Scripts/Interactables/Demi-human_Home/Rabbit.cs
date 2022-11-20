using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Conversable
{
    public Conversation convo1, convo2;
    public static bool talked;

    public override void Interact()
    {
        if (!talked)
        {
            dialogueManager.StartConversation(convo1);        
            talked = true;
            return;
        }

        dialogueManager.StartConversation(convo2);
    }
}
