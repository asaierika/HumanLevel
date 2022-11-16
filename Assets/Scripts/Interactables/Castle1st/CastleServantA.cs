using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleServantA : Interactable
{
    public Conversation convo1, convo2, convo3;
    public Choice choice1, choice2;
    public DialogueManager dialogueManager;
    public ChoiceManager choiceManager;

    void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        dialogueManager.StartConversation(convo1);
        choiceManager.StartChoice(choice1, choice2);
    }

    public void Choice1()
    {
        dialogueManager.StartConversation(convo2);
    }

    public void Choice2()
    {
        dialogueManager.StartConversation(convo3);
    }
}
