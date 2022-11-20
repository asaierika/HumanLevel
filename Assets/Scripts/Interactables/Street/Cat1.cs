public class Cat1 : Conversable
{
    public Conversation convo;

    public override void Interact()
    {
        // specific things to do when the player is interacting with the object      
        dialogueManager.StartConversation(convo);

    }
}
