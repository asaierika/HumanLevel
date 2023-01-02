public class Cat2 : Interactable
{
    public Conversation convo1, convo2, convo3, convo4;
    public Item fish, hairPin;
    public bool fed;
    private Inventory inventory;
    public FishSeller seller;

    void Start() {
        inventory = GameManager.instance.inventory;
    }

    public override void Interact()
    {
        if (fed)
        {
            DialogueManager.instance.StartConversation(convo4);            
            return;
        }
        if (inventory.Contains(fish))
        {
            DialogueManager.instance.StartConversation(convo2);
            return;
        }

        DialogueManager.instance.StartConversation(convo1);
        seller.sawCat = true;
    }

    public void UseFishChunk()
    {
        if (!playerInRange)
        return;

        DialogueManager.instance.StartConversation(convo3);
        fed = true;
        inventory.Add(hairPin);
        inventory.Remove(fish);
    }
}
