using UnityEngine;

// All data that must be persisted through scene in the session will be stored here.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Inventory inventory;
    public bool playerFrozen;
    public bool playerInitialised;
    // TODO: Delegate all scene transition to one class
    public string lastScene;

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
}
