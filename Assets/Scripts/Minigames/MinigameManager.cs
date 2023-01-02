using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// At most one minigame being played by each playable character at a given moment.
public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;
    [SerializeField]
    private MinigameID kizunaActiveMinigameId;
    [SerializeField]
    private MinigameID partnerActiveMinigameId;
    // private MinigameID activeMinigameID = MinigameID.NONE;
    [SerializeField]
    private Pair<MinigameID, Minigame>[] gameIdsToGame;
    public enum MinigameID {
        NONE,
        TURNTABLE,
        DRAGON_PUZZLE,
    }
    private Dictionary<MinigameID, Minigame> minigameTable = new Dictionary<MinigameID, Minigame>();

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
        if (Input.GetKeyDown(KeyCode.Escape)) {
            switch (StateManager.instance.GetCurrentIdentity()) {
                case ValidPlayerState.Who.KIZUNA:
                    if (kizunaActiveMinigameId != MinigameID.NONE) {
                        minigameTable[kizunaActiveMinigameId].GetComponent<Minigame>().OnKeyboardExit();
                    }
                    return;
                case ValidPlayerState.Who.PARTNER:
                    if (partnerActiveMinigameId != MinigameID.NONE) {
                        minigameTable[partnerActiveMinigameId].GetComponent<Minigame>().OnKeyboardExit();
                    }
                    return;
                default:
                    Debug.LogError("Invalid character state");
                    return;
            }
        }
    }

    private void InitMinigameTable() {
        foreach (Pair<MinigameID, Minigame> gameIDToGame in gameIdsToGame) {
            minigameTable.Add(gameIDToGame.head, gameIDToGame.tail);
        }
    }

    public void StartMinigame(MinigameID minigameID) {
        ValidPlayerState.Who playingCharacter = StateManager.instance.GetCurrentIdentity();

        switch (playingCharacter) {
            case ValidPlayerState.Who.KIZUNA:
                EventManager.InvokeEvent(EventManager.Event.KIZUNA_MINIGAME_START);
                if (kizunaActiveMinigameId != MinigameID.NONE) {
                    Debug.LogError("There cannot be two minigames active simultaneously");
                }
                minigameTable[minigameID].gameObject.SetActive(true);
                kizunaActiveMinigameId = minigameID;
                return;
            case ValidPlayerState.Who.PARTNER:
                EventManager.InvokeEvent(EventManager.Event.PARTNER_MINIGAME_START);
                if (partnerActiveMinigameId != MinigameID.NONE) {
                    Debug.LogError("There cannot be two minigames active simultaneously");
                }
                minigameTable[minigameID].gameObject.SetActive(true);
                partnerActiveMinigameId = minigameID;
                return;
        }

        Debug.LogError("Invalid character state");
    }

    // Invoked when character switch occurs before minigame is exited or completed
    public void PauseMinigame() {
        ValidPlayerState.Who pausingCharacter = StateManager.instance.GetCurrentIdentity();

        switch (pausingCharacter) {
            case ValidPlayerState.Who.KIZUNA:
                EventManager.InvokeEvent(EventManager.Event.KIZUNA_MINIGAME_PAUSE);
                if (kizunaActiveMinigameId != MinigameID.NONE) {
                    Debug.LogError("There cannot be two minigames active simultaneously");
                }
                minigameTable[kizunaActiveMinigameId].gameObject.SetActive(false);
                return;
            case ValidPlayerState.Who.PARTNER:
                EventManager.InvokeEvent(EventManager.Event.PARTNER_MINIGAME_PAUSE);
                if (partnerActiveMinigameId != MinigameID.NONE) {
                    Debug.LogError("There cannot be two minigames active simultaneously");
                }
                minigameTable[partnerActiveMinigameId].gameObject.SetActive(false);
                return;
        }
    }

    // Invoked when there is an ongoing minigame for the character that is being switched to
    public void ResumeMinigame(object inputParam = null) {
        ValidPlayerState.Who resumingCharacter = StateManager.instance.GetCurrentIdentity();

        switch (resumingCharacter) {
            case ValidPlayerState.Who.KIZUNA:
                EventManager.InvokeEvent(EventManager.Event.KIZUNA_MINIGAME_RESUME);
                if (kizunaActiveMinigameId != MinigameID.NONE) {
                    Debug.LogError("There cannot be two minigames active simultaneously");
                }
                minigameTable[kizunaActiveMinigameId].gameObject.SetActive(true);
                return;
            case ValidPlayerState.Who.PARTNER:
                EventManager.InvokeEvent(EventManager.Event.PARTNER_MINIGAME_RESUME);
                if (partnerActiveMinigameId != MinigameID.NONE) {
                    Debug.LogError("There cannot be two minigames active simultaneously");
                }
                minigameTable[partnerActiveMinigameId].gameObject.SetActive(true);
                return;
        }

        Debug.LogError("Invalid character state");
    }

    public void ExitMinigame(object inputParam = null) {
        ValidPlayerState.Who exitingCharacter = StateManager.instance.GetCurrentIdentity();

        switch (exitingCharacter) {
            case ValidPlayerState.Who.KIZUNA:
                EventManager.InvokeEvent(EventManager.Event.KIZUNA_MINIGAME_END);
                minigameTable[kizunaActiveMinigameId].gameObject.SetActive(false);
                kizunaActiveMinigameId = MinigameID.NONE;
                return;
            case ValidPlayerState.Who.PARTNER:
                EventManager.InvokeEvent(EventManager.Event.PARTNER_MINIGAME_END);
                minigameTable[partnerActiveMinigameId].gameObject.SetActive(false);
                partnerActiveMinigameId = MinigameID.NONE;
                return;
        }

        Debug.LogError("Invalid character state");
    }
}
