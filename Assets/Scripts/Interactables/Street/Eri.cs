using UnityEngine;

public class Eri : Conversable
{
    public static bool talked;
    public Item hairPin;
    public Conversation convo1, convo2;
    public GameObject timeline;
    public Inventory inventory;

    void Start() {
        inventory = GameManager.instance.inventory;
    }

    private void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        if (inventory.Contains(hairPin))
        {
            timeline.SetActive(true);
            inventory.Remove(hairPin);
            return;
        }

        if (talked)
        {
            dialogueManager.StartConversation(convo2);
            return;
        }

        dialogueManager.StartConversation(convo1);
        talked = true;
    }
}
