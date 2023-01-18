using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    private void Awake() 
    {
        if (ChoiceManager.instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private string choice1, choice2;
    public GameObject choiceHolder;
    private Button[] buttons;
    public GameObject firstButton;
    private int choiceIndex = -1;
    private bool isActive;

    // Update is called once per frame
    void Update()
    {
        if (isActive && !DialogueManager.instance.inDialogue)
        {
            isActive = false;
            UiStatus.OpenUI();
            choiceHolder.SetActive(true);
            buttons = choiceHolder.GetComponentsInChildren<Button>();
            buttons[0].GetComponentInChildren<Text>().text = choice1;
            buttons[1].GetComponentInChildren<Text>().text = choice2;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }
    
    public void StartChoice(string choice1, string choice2)
    {
        isActive = true;
        this.choice1 = choice1;
        this.choice2 = choice2;
    }

    public void SetChoice(int i)
    {
        choiceIndex = i;

        if (choiceIndex == 0)
        {
            //choice1.TriggerEvent();
            EventManager.InvokeEvent(EventManager.Event.CHOICE_ONE);
        }
        else if (choiceIndex == 1)
        {
            EventManager.InvokeEvent(EventManager.Event.CHOICE_TWO);

        }
        choiceIndex = -1;
        InputManager.instance.choiceButtonActivated = true;
        choiceHolder.SetActive(false);
        EventManager.StopListeningAll(EventManager.Event.CHOICE_ONE);
        EventManager.StopListeningAll(EventManager.Event.CHOICE_TWO);
    }
}
