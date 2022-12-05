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
            DialogueManager.instance.StartConversation(convo1);        
            talked = true;
            return;
        }

        DialogueManager.instance.StartConversation(convo2);
    }
}
