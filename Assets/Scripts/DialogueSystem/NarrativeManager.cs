using UnityEngine;
using UnityEngine.UI;

// Same as Dialogue Manager but with a larger dialogue box
// and no speaker. Used for stories etc.
public class NarrativeManager : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogue;
    private int currIndex;
    private Conversation currentConvo;
    private static NarrativeManager instance;
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

        instance.ReadNext();
    }

    public void ReadNext()
    {
        if (currIndex >= currentConvo.allLines.Length)
        {
            instance.dialogBox.transform.localScale = Vector3.zero;
            inDialogue = false;
            GameEvents.instance.CloseUI();
        }
        else
        {
            dialogue.text = currentConvo.allLines[currIndex].dialogue;
            currIndex++;
        }
    }
}
