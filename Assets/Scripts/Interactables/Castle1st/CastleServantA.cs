using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleServantA : Interactable
{
    public Conversation convo1, convo2, convo3;
    public string choice1, choice2;
    void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        DialogueManager.instance.StartConversation(convo1);
        EventManager.StartListening(EventManager.Event.CHOICE_ONE, Choice1);
        EventManager.StartListening(EventManager.Event.CHOICE_TWO, Choice2);
        ChoiceManager.instance.StartChoice(choice1, choice2);
    }

    public void Choice1(object o = null)
    {
        DialogueManager.instance.StartConversation(convo2);
    }

    public void Choice2(object o = null)
    {
        DialogueManager.instance.StartConversation(convo3);
    }
}
