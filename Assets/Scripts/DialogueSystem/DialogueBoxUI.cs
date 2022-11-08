using UnityEngine;

public class DialogueBoxUI : MonoBehaviour
{
    public GameObject DialogBox;

    DialogueManager dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = DialogueManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
