using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Placed under a singleton parent
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogBox;
    public Text speakerName, dialogue;
    public Image speakerSprite;
    private int currIndex;
    private Conversation currentConvo;
    public static bool inDialogue = false;
    public UiStatus uiStatus;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && inDialogue)
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
        if (currIndex >= currentConvo.allLines.Length)
        {    
            uiStatus.CloseUI();
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
        yield return new WaitForSeconds(0.01f);
        inDialogue = false;
        
        dialogBox.SetActive(false);   
    }
}
