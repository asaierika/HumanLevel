using System.Collections;
using System.Collections.Generic;
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
        //dialogBox.transform.localScale = Vector3.zero;
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
        //Debug.Log(inDialogue);
        if (Input.GetKeyDown(KeyCode.Z) && inDialogue)
        {
            instance.ReadNext();

        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.dialogBox.SetActive(true);
        GameEvents.instance.OpenUI();
        
        //instance.dialogBox.transform.localScale = Vector3.one;
        instance.currIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";

        instance.ReadNext();     
        inDialogue = true;  
    }

    public void ReadNext()
    {
        if (currIndex >= currentConvo.allLines.Length)
        {     
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
        GameEvents.instance.CloseUI();
        dialogBox.SetActive(false);   
    }
}
