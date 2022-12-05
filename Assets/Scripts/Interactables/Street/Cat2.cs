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
            DialogueManager.instance.StartConversation(convo3);            
            return;
        }
        if (inventory.Contains(fish))
        {
            DialogueManager.instance.StartConversation(convo2);
            fed = true;
            inventory.Add(hairPin);
            inventory.UseItem(fish);
            return;
        }

        DialogueManager.instance.StartConversation(convo1);
        seller.sawCat = true;
    }
}
