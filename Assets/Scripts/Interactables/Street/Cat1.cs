public class Cat1 : Interactable
{
    public Conversation convo;
    
    void Update()
    {
        // every subclass of Interactables calls TryInteract() in Update()
        TryInteract();
    }

    public override void Interact()
    {
        // specific things to do when the player is interacting with the object      
        DialogueManager.StartConversation(convo);

    }
}
