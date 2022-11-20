using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A block in the position of a portal when the player tries to
// access. Shows some words as Conversation to inform the player 
// that tranporting to another scene is blocked.
public class Block : Conversable
{
    public Conversation convo;

    public override void Interact()
    {
        dialogueManager.StartConversation(convo);
    }
}
