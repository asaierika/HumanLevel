using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Placed under a singleton parent
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake() 
    {
        if (DialogueManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    
    public GameObject dialogBox;
    public Text speakerName, dialogue;
    public Image speakerSprite;
    private int currIndex;
    private Conversation currentConvo;
    public static bool inDialogue = false;
    public UiStatus uiStatus;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && inDialogue)
        {
            ReadNext();
        }
    }

    public void StartConversation(Conversation convo)
    {
        dialogBox.SetActive(true);
        uiStatus.OpenUI();
        
        currIndex = 0;
        currentConvo = convo;
        speakerName.text = "";
        dialogue.text = "";

        ReadNext();    
        inDialogue = true;  
    }

    public void ReadNext()
    {
        if (currIndex == currentConvo.allLines.Length)
        {    
            Debug.Log("Total lines is " + currentConvo.allLines.Length + " No more lines at " + currIndex);
            uiStatus.CloseUI();
            EndDialogue();      
        }
        else 
        {
            Debug.Log("Reading next line " + currIndex);
            speakerName.text = currentConvo.allLines[currIndex].speaker.speakerName;
            dialogue.text = currentConvo.allLines[currIndex].dialogue;
            speakerSprite.sprite = currentConvo.allLines[currIndex].speaker.speakerSprite;
            currIndex++;
        }
    }

    // EXPLANATION: Perviously when "Z" is pressed at the last line of conversation,
    // the TryInteract() method will catch the signal whilst inDialogue might have already been set to false.
    public void EndDialogue() {
        Debug.Log("Ending conversation.");
        inDialogue = false;
        dialogBox.SetActive(false);
    }
}
