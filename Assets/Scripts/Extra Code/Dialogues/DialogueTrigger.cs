using UnityEngine;

// TODO: Remove this class/
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            // Debug.Log("Conversation started");
            GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DemiHumanDialogueManager>().StartDialogue(dialogue);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            // Debug.Log("Conversation started");
            GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DemiHumanDialogueManager>().StartDialogue(dialogue);
        }
    }
}
