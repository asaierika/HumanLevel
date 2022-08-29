using UnityEngine;

public class Eri : Interactable
{
    public static bool talked;
    public Item hairPin;
    public Conversation convo1, convo2;
    public GameObject timeline;

    private void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        if (Inventory.instance.Contains(hairPin))
        {
            timeline.SetActive(true);
            Inventory.instance.Remove(hairPin);
            return;
        }

        if (talked)
        {
            DialogueManager.StartConversation(convo2);
            return;
        }

        DialogueManager.StartConversation(convo1);
        talked = true;
    }
}
