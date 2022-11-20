public class Cat2 : Conversable
{
    public Conversation convo1, convo2, convo3;
    public Item fish, hairPin;
    public bool fed;
    public Inventory inventory;
    public FishSeller seller;

    void Start() {
        inventory = GameManager.instance.inventory;
    }

    public override void Interact()
    {
        if (fed)
        {
            dialogueManager.StartConversation(convo3);            
            return;
        }
        if (inventory.Contains(fish))
        {
            dialogueManager.StartConversation(convo2);
            fed = true;
            inventory.Add(hairPin);
            inventory.Remove(fish);
            return;
        }

        dialogueManager.StartConversation(convo1);
        seller.sawCat = true;
    }
}
