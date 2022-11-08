using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Many Game Events come in pairs hence the template.
*/
[CreateAssetMenu(menuName="Pair Event")]
public class PairEvent : ScriptableObject
{
    public GameEvent PositiveEvent;
    public GameEvent NegativeEvent;
    
    public void TriggerPositive() {
        PositiveEvent.TriggerEvent();
    }

    public void TriggerNegative() {
        NegativeEvent.TriggerEvent();
    }
}
