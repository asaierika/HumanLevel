using UnityEngine;

public class Eri : Interactable
{
    public static bool talked;
    public Item hairPin;
    public Conversation convo1, convo2, convo3;
    public GameObject timeline;
    public Inventory inventory;

    void Start() {
        inventory = GameManager.instance.inventory;
    }

    public override void Interact()
    {
        if (inventory.Contains(hairPin))
        {
            DialogueManager.instance.StartConversation(convo3);
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

    public void UseHairPin()
    {
        if (!playerInRange)
        return;
        
        timeline.SetActive(true);
        inventory.Remove(hairPin);
    }
}
