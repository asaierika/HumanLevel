public class FishSeller : Conversable
{
    public bool sawCat, givenFish;
    public Item fish;
    public Conversation convo1, convo2;
    public Inventory inventory;

    void Start() {
        inventory = GameManager.instance.inventory;
    }

    void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        if (givenFish)
        {
            DialogueManager.instance.StartConversation(convo1);
            return;
        }
        if (sawCat && Eri.talked)
        {
            DialogueManager.instance.StartConversation(convo2);
            inventory.Add(fish);
            givenFish = true;
            return;
        }

        DialogueManager.instance.StartConversation(convo1);
    }
}
