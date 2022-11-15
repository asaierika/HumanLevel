using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleServantA : Interactable
{
    public Conversation convo1, convo2, convo3;
    public Choice choice1, choice2;

    void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        DialogueManager.StartConversation(convo1);
        ChoiceManager.instance.StartChoice(choice1, choice2);
    }

    public void Choice1()
    {
        DialogueManager.StartConversation(convo2);
    }

    public void Choice2()
    {
        DialogueManager.StartConversation(convo3);
    }
}
