using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;
    [SerializeField]
    private MinigameID activeMinigameID = MinigameID.NONE;
    [SerializeField]
    private Pair<MinigameID, GameObject>[] gameIdsToGameObjects;
    public enum MinigameID {
        NONE,
        TURNTABLE,
        DRAGON_PUZZLE,
    }
    public Dictionary<MinigameID, GameObject> minigameTable = new Dictionary<MinigameID, GameObject>();

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        InitMinigameTable();
    }

    // NOTE: Not needed (defensive)
    void OnDestroy() {
        minigameTable.Clear();
    }

    void Update() {
        if (activeMinigameID != MinigameID.NONE && Input.GetKeyDown(KeyCode.Escape)) {
            minigameTable[activeMinigameID].GetComponent<Minigame>().OnKeyboardExit();
        } 
    }

    private void InitMinigameTable() {
        foreach (Pair<MinigameID, GameObject> gameIDToGameObject in gameIdsToGameObjects) {
            minigameTable.Add(gameIDToGameObject.head, gameIDToGameObject.tail);
        }
    }

    public void StartMinigame(MinigameID minigameID, CharacterSwitcher.Who gameOwner) {
        EventManager.InvokeEvent(EventManager.Event.MINIGAME_START);
        if (activeMinigameID != MinigameID.NONE) {
            Debug.LogError("There cannot be two minigames active simultaneously");
        }
        minigameTable[minigameID].SetActive(true);
        activeMinigameID = minigameID;
        Minigame minigame = minigameTable[minigameID].GetComponent<Minigame>();
        minigame.gameOwner = gameOwner;
    }

    public void ExitMinigame(object inputParam = null) {
        EventManager.InvokeEvent(EventManager.Event.MINIGAME_END);
        // NOTE: Can be more defensive and check
        minigameTable[activeMinigameID].SetActive(false);
        minigameTable[activeMinigameID].GetComponent<Minigame>().gameOwner = CharacterSwitcher.Who.None;
        activeMinigameID = MinigameID.NONE;
    }
}
