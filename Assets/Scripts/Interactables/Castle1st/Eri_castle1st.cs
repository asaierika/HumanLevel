
using UnityEngine;

public class Eri_castle1st : Conversable
{
    public Conversation convo;
    public Choice choice1, choice2;
    public Vector2 hallPosition;
    public GameEvent chaseStarted;

    public override void Interact()
    {
        dialogueManager.StartConversation(convo);
        choiceManager.StartChoice(choice1, choice2);
    }

    public void Choice1()
    {
        PlayerSpawner.AssignSpawnPoint(hallPosition);
        chaseStarted.TriggerEvent();
        SceneTransition.SceneTransit("Hall");
    }
}
