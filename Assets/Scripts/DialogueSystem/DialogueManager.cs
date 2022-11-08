using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogBox;
    public Text speakerName, dialogue;
    public Image speakerSprite;
    private int currIndex;
    private Conversation currentConvo;
    public static DialogueManager instance;
    public static bool inDialogue = false;

    private void Awake()
    {
        dialogBox.transform.localScale = Vector3.zero;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (inDialogue)
            instance.ReadNext();
        }
    }

    public static void StartConversation(Conversation convo)
    {
        GameEvents.instance.OpenUI();
        inDialogue = true;
        instance.dialogBox.transform.localScale = Vector3.one;
        instance.currIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";

        instance.ReadNext();       
    }

    public void ReadNext()
    {
        if (currIndex >= currentConvo.allLines.Length)
        {
            instance.dialogBox.transform.localScale = Vector3.zero;    
            StartCoroutine(EndDialogue());       
        }
        else
        {
            speakerName.text = currentConvo.allLines[currIndex].speaker.speakerName;
            dialogue.text = currentConvo.allLines[currIndex].dialogue;
            speakerSprite.sprite = currentConvo.allLines[currIndex].speaker.speakerSprite;
            currIndex++;
        }
    }

    // For some reason, need to wait for some time 
    // before setting inDialogue to false and call 
    // CloseUI, otherwise a new dialogue would be 
    // triggered as the player stands in the trigger
    // zone of the interactable and presses 'z'. 
    IEnumerator EndDialogue() {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("isDialogue = false");
        inDialogue = false;
        GameEvents.instance.CloseUI();
    }

}
