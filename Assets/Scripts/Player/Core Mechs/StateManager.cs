using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    [SerializeField] private ValidPlayerState currPlayerState;
    public ValidPlayerState CurrPlayerState { 
        get { return currPlayerState; } 
        set { currPlayerState = value; }
    }
    // Add states that should not be transitioned to for a given period to this set
    public HashSet<ValidPlayerState> temporaryUnavailableStates = new HashSet<ValidPlayerState>();

    void Awake() {
        if (instance == null) {
            instance = this;
            CurrPlayerState = ValidPlayerState.KizunaDemi;
            Debug.Log($"New State {CurrPlayerState}");
            Debug.Log($"Next Mode Switch State {CurrPlayerState.NextModeSwitchState}");
            Debug.Log($"Next Char Switch State {CurrPlayerState.NextCharacterSwitchState}");
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            SwitchCharacter();
        } else if (Input.GetKeyDown(KeyCode.R)) {
            // Keyboard switch not allowed in spirit mode
            if (currPlayerState.Mode != ValidPlayerState.Form.SPIRIT) SwitchMode();
        }
    }

    public void SwitchCharacter() {
        ValidPlayerState nextPlayerState = CurrPlayerState.NextCharacterSwitchState;
        if (!temporaryUnavailableStates.Contains(nextPlayerState) && nextPlayerState != CurrPlayerState) {
            CurrPlayerState = nextPlayerState;
            Debug.Log($"New State {CurrPlayerState}");
            EventManager.InvokeEvent(CurrPlayerState.StateMounted, currPlayerState);
        }
    }

    public void SwitchMode() {
        ValidPlayerState nextPlayerState = CurrPlayerState.NextModeSwitchState;
        if (!temporaryUnavailableStates.Contains(nextPlayerState) && nextPlayerState != CurrPlayerState) {
            CurrPlayerState = nextPlayerState;
            Debug.Log($"New State {CurrPlayerState}");
            EventManager.InvokeEvent(CurrPlayerState.StateMounted, currPlayerState);
        }
    }

    public ValidPlayerState.Who GetCurrentIdentity() {
        return CurrPlayerState.Identity;
    }

    public ValidPlayerState.Form GetCurrentForm() {
        return CurrPlayerState.Mode;
    }
}
