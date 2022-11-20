using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueContainer : ScriptableObject
{
    public string[] names;
    [TextArea(3, 10)]
    public string[] sentences;
}
