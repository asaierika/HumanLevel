using UnityEngine;

public class Tester : MonoBehaviour
{
    public Conversation convo;
    public DialogueManager dialogueManager;

    public void StartConvo()
    {
        dialogueManager.StartConversation(convo);
    }
}
