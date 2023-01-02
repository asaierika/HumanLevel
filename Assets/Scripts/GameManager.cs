using UnityEngine;

// All data that must be persisted through scene in the session will be stored here.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Inventory inventory;
    // TODO: Delegate all scene transition to one class
    public string lastScene;
    // The submitButton(currently 'Z') can be detected by multiple scripts
    // such as Item.Use() and Interactable.TryInteract(). To avoid the 
    // submit button being detected by more than one script, create a boolean 
    //  submitButtonDetect in GamaManager that is set to true every time
    // the submit button is pressed, and set to false when one of the sript
    // detects it.
    // Currently, the submit button is used by Item.Use(),
    // DialogueManager.Update() and Interactable.TryInteract().
    public bool itemUseButtonActivated;
    public bool interactButtonActivated;
    public bool dialogButtonActivated;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
       
        DontDestroyOnLoad(gameObject);

        instance = this;
    }

    // Priority: itemUseButtonActivated > dialogButtonActivated > interactButtonActivated
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!itemUseButtonActivated && PlayerMovement.PLAYER_FROZEN)
            {
                dialogButtonActivated = true;
            } 

            if (!itemUseButtonActivated && !dialogButtonActivated)
            {
                interactButtonActivated = true;
            }
        }
        else 
        {
            itemUseButtonActivated = false;
            interactButtonActivated = false;
            dialogButtonActivated = false;
        }
    }
}
