
using UnityEngine;

public class Eri_castle1st : Interactable
{
    public Conversation convo1, convo2;
    public Choice choice1, choice2;
    public Vector2 hallPosition;
    public GameEvent chaseStarted;

    public override void Interact()
    {
        DialogueManager.instance.StartConversation(convo1);
        ChoiceManager.instance.StartChoice(choice1, choice2);
    }

    public void Choice1()
    {
        PlayerSpawner.AssignSpawnPoint(hallPosition);
        chaseStarted.TriggerEvent();
        SceneTransition.SceneTransit("Hall");
    }

    public void Choice2()
    {
        DialogueManager.instance.StartConversation(convo2);
    }
}
