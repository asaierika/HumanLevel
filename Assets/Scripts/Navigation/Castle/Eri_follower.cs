using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_follower : Follower
{
    public Conversation convo;
    public string choice1, choice2;
    public GameEvent chaseEnded;
    public Vector2 hallPosition;

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    protected override void Found()
    {
        // Eri is dynamically generated at each room. Both managers cannot be assigned before hand.
        DialogueManager.instance.StartConversation(convo);
        EventManager.StartListening(EventManager.Event.CHOICE_ONE, Choice1);
        EventManager.StartListening(EventManager.Event.CHOICE_TWO, Choice2);
        ChoiceManager.instance.StartChoice(choice1, choice2);
        chaseEnded.TriggerEvent();
    }

    public void Choice1(object o = null)
    {
        PlayerSpawner.AssignSpawnPoint(hallPosition);
        Destroy(FollowingManager.instance);
        SceneTransition.SceneTransit("Hall");
    }

    public void Choice2(object o = null)
    {
        PlayerSpawner.AssignSpawnPoint(hallPosition);
        // Remove the singleton
        Destroy(FollowingManager.instance);
        SceneTransition.SceneTransit("Hall_Before_Chase");
    }
}
