public class FishSeller : Conversable
{
    public static bool sawCat, givenFish;
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
            dialogueManager.StartConversation(convo1);
            return;
        }
        if (sawCat && Eri.talked)
        {
            dialogueManager.StartConversation(convo2);
            inventory.Add(fish);
            givenFish = true;
            return;
        }

        dialogueManager.StartConversation(convo1);
    }
}
