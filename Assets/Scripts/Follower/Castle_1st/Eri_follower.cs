using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_follower : Follower
{
    public Conversation convo;
    public Choice choice1, choice2;
    public Conversable reaction;
    private static bool isFollowing;

    protected override void Found()
    {
        reaction.dialogueManager.StartConversation(convo);
        reaction.choiceManager.StartChoice(choice1, choice2);
    }
}
