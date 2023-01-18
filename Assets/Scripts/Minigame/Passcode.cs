using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

/**
Passslot Class that wraps 
- TextMesh + 2 Button 
- Getter/Setter
- Correct Digit?
- Action (OnCorrect)

Password Class 
- Counter 
- Adding onCorrectListener
- Method (To increment/decrement the counter)
**/




public class Passcode : MonoBehaviour
{
    [SerializeField] PasscodeSO passcode;

    [SerializeField] TextMeshProUGUI[] slots = new TextMeshProUGUI[4];
    [SerializeField] Button slot1Increment;
    [SerializeField] Button slot1Decrement;
    [SerializeField] Button slot2Increment;
    [SerializeField] Button slot2Decrement;
    [SerializeField] Button slot3Increment;
    [SerializeField] Button slot3Decrement;
    [SerializeField] Button slot4Increment;
    [SerializeField] Button slot4Decrement;



    void Start() {
        slots[0].text = passcode.GetCodeIndex(0);
        slots[1].text = passcode.GetCodeIndex(1);
        slots[2].text = passcode.GetCodeIndex(2);
        slots[3].text = passcode.GetCodeIndex(3);
    }

    void OnEnable() {
        slot1Increment.onClick.AddListener(() => OnButtonClick(0, true));
        slot1Decrement.onClick.AddListener(() => OnButtonClick(0, false));

        slot2Increment.onClick.AddListener(() => OnButtonClick(1, true));
        slot2Decrement.onClick.AddListener(() => OnButtonClick(1, false));

        slot3Increment.onClick.AddListener(() => OnButtonClick(2, true));
        slot3Decrement.onClick.AddListener(() => OnButtonClick(2, false));

        slot4Increment.onClick.AddListener(() => OnButtonClick(3, true));
        slot4Decrement.onClick.AddListener(() => OnButtonClick(3, false));
    }

    private void OnButtonClick(int index, bool order) {
        
        int i = int.Parse(passcode.GetCodeIndex(index));
        if(order) {
            if (i == 9) {
                i = 0;
            }else {
                i += 1;
            }
        }else {
            if (i == 0) {
                i = 9;
            }else {
                i -= 1;
            }
        }

        passcode.setCodeIndex(index, i.ToString());
        slots[index].text = i.ToString();
        Debug.Log(i);
    }

    void OnDisable() {
        slot1Increment.onClick.RemoveAllListeners();
        slot1Decrement.onClick.RemoveAllListeners();

        slot2Increment.onClick.RemoveAllListeners();
        slot2Decrement.onClick.RemoveAllListeners();

        slot3Increment.onClick.RemoveAllListeners();
        slot3Decrement.onClick.RemoveAllListeners();

        slot4Increment.onClick.RemoveAllListeners();
        slot4Decrement.onClick.RemoveAllListeners();
    }

}
