using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_follower : Follower
{
    public Conversation convo;
    public Choice choice1, choice2;
    public GameEvent chaseEnded;

    protected override void Found()
    {
        // Eri is dynamically generated at each room. Both managers cannot be assigned before hand.
        GameObject.FindObjectOfType<DialogueManager>().StartConversation(convo);
        GameObject.FindObjectOfType<ChoiceManager>().StartChoice(choice1, choice2);
        chaseEnded.TriggerEvent();
    }
}
