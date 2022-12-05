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

    public override void Interact()
    {
        if (inventory.Contains(hairPin))
        {
            timeline.SetActive(true);
            inventory.UseItem(hairPin);
            return;
        }

        if (talked)
        {
            DialogueManager.instance.StartConversation(convo2);
            return;
        }

        DialogueManager.instance.StartConversation(convo1);
        talked = true;
    }
}
