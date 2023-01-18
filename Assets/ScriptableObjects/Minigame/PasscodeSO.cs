using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Passcode Answer", fileName = "New Passcode")]
public class PasscodeSO : ScriptableObject
{

    [SerializeField] string code = "0000";
    [SerializeField] string[] currentVal = new string[4];

    public string GetCodeIndex(int index) {
        return currentVal[index];
    }

    public void setCodeIndex(int index, string val) {
        currentVal[index] = val;
    }

    public bool CheckAnswer() {
        string val = "";
        foreach(string s in currentVal) {
            val += s;
        }

        return code == val;
    }
}

