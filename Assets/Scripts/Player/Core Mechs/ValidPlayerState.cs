using System;
using UnityEngine;

// All valid player states should be specified here as static fields.
[Serializable]
public class ValidPlayerState
{
    public static ValidPlayerState KizunaDemi ;
    public static ValidPlayerState KizunaSpirit;
    public static ValidPlayerState PartnerDemi;
    [SerializeField]
    private Who identity;
    public Who Identity { 
        get { return identity; }
        set { identity = value; } 
    }
    [SerializeField]
    private Form mode;
    public Form Mode { 
        get { return mode; }
        set { mode = value; } 
    }
    [SerializeField]
    private EventManager.Event stateMounted;
    public EventManager.Event StateMounted { 
        get { return stateMounted; }
        set { stateMounted = value; }
    }
    // State to transition to when mode switch is triggered
    public ValidPlayerState NextModeSwitchState { get; set; }
    // State to transition to when character switch is triggered
    public ValidPlayerState NextCharacterSwitchState { get; set; }

    public enum Who {
        NONE,
        KIZUNA,
        PARTNER
    }

    public enum Form { 
        NONE,
        DEMIHUMAN, 
        SPIRIT
    }

    static ValidPlayerState() {
        KizunaDemi = new ValidPlayerState(Who.KIZUNA, Form.DEMIHUMAN, EventManager.Event.SWITCH_TO_KIZUNA_DEMI);
        KizunaSpirit = new ValidPlayerState(Who.KIZUNA, Form.SPIRIT, EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT);
        PartnerDemi = new ValidPlayerState(Who.PARTNER, Form.DEMIHUMAN, EventManager.Event.SWITCH_TO_PARTNER_DEMI);
        KizunaDemi.NextCharacterSwitchState = PartnerDemi;
        KizunaDemi.NextModeSwitchState = KizunaSpirit;
        // Character switch prohibited in spirit mode
        KizunaSpirit.NextCharacterSwitchState = KizunaSpirit;
        KizunaSpirit.NextModeSwitchState = KizunaDemi;
        // Mode switch prohibited when playing as partner
        PartnerDemi.NextCharacterSwitchState = KizunaDemi;
        PartnerDemi.NextModeSwitchState = PartnerDemi;
    }

    private ValidPlayerState(Who character, Form mode, EventManager.Event stateMounted) {
        this.Identity = character;
        this.Mode = mode;
        this.StateMounted = stateMounted;
    }

    public override int GetHashCode() {
        return HashCode.Combine<Who, Form>(Identity, Mode);
    }

    public override bool Equals(object obj) {
        if (obj is ValidPlayerState) {
            ValidPlayerState otherState = (ValidPlayerState)obj;
            return otherState.Identity == this.Identity && otherState.Mode == this.Mode;
        }

        return false;
    }

    public override string ToString() {
        return Identity.ToString() + " " + Mode.ToString();
    }
}
