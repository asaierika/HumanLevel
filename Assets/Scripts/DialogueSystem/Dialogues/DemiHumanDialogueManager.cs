using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemiHumanDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private bool inConversation = false;
    private Queue<string> names = new Queue<string>();
    private Queue<string> sentences = new Queue<string>();
    private GameEvent followup;
    public Animator animator;

    void Update() {
        if (inConversation && Input.GetKeyDown(KeyCode.Z)) {
            DisplayNextSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("IsOpen", true);
        inConversation = true;
        sentences.Clear();
        names.Clear();
        followup = dialogue.followup;

        foreach (string name in dialogue.names) {
            names.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        nameText.text = names.Dequeue();

        StopAllCoroutines();
        StartCoroutine(Speak(sentences.Dequeue()));
    }

    IEnumerator Speak(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue() {
        inConversation = false;
        animator.SetBool("IsOpen", false);
        followup.TriggerEvent();
    }
}
