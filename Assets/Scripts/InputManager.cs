using UnityEngine;

public class InputManager : MonoBehaviour 
{
    public static InputManager instance;

    private void Awake()
    {
        if (InputManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
       
        DontDestroyOnLoad(gameObject);

        instance = this;
    }

    // The submitButton(currently 'J') can be detected by multiple scripts
    // such as Item.Use() and Interactable.TryInteract(). To avoid the 
    // submit button being detected by more than one script, create a boolean 
    //  submitButtonDetect in GamaManager that is set to true every time
    // the submit button is pressed, and set to false when one of the sript
    // detects it.
    // Currently, the submit button is used by Item.Use(), DialogueManager.Update() 
    // and Interactable.TryInteract() and ChoiceManager.SetChoice().
    public bool itemUseButtonActivated;
    public bool interactButtonActivated;
    public bool dialogButtonActivated;
    public bool choiceButtonActivated;

     // Priority: itemUseButtonActivated > dialogButtonActivated = choiceButtonActivated > interactButtonActivated
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!itemUseButtonActivated && CharacterMovement.playerFrozen)
            {
                dialogButtonActivated = true;
            } 

            if (!itemUseButtonActivated && !dialogButtonActivated && !choiceButtonActivated)
            {
                interactButtonActivated = true;
            }
        }
        else 
        {
            itemUseButtonActivated = false;
            interactButtonActivated = false;
            dialogButtonActivated = false;
            choiceButtonActivated = false;
        }
    }
}