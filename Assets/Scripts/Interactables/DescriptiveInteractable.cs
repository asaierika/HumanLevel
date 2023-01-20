// Interactables that has the simple function of showing a conversation.
public class DescriptiveInteractable : Interactable
{
    public Conversation convo;

    public override void Interact()
    {
        // specific things to do when the player is interacting with the object      
        DialogueManager.instance.StartConversation(convo);
    }
}