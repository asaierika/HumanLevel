
using UnityEngine;

public class Eri_castle1st : Interactable
{
    public Conversation convo1, convo2;
    public string choice1, choice2;
    public Vector2 hallPosition;
    public GameEvent chaseStarted;

    public override void Interact()
    {
        DialogueManager.instance.StartConversation(convo1);
        EventManager.StartListening(EventManager.Event.CHOICE_ONE, Choice1);
        EventManager.StartListening(EventManager.Event.CHOICE_TWO, Choice2);
        ChoiceManager.instance.StartChoice(choice1, choice2);
    }

    public void Choice1(object o = null)
    {
        PlayerSpawner.AssignSpawnPoint(hallPosition);
        chaseStarted.TriggerEvent();
        SceneTransition.SceneTransit("Hall");
    }

    public void Choice2(object o = null)
    {
        DialogueManager.instance.StartConversation(convo2);
    }
}
