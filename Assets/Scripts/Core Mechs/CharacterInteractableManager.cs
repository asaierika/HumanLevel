using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractableManager : MonoBehaviour
{
    public Dictionary<SwitchCharacter.Who, List<GameObject>> possessions = new Dictionary<SwitchCharacter.Who, List<GameObject>>();

    void Awake() {
        possessions.Add(SwitchCharacter.Who.Kizuna, new List<GameObject>());
        possessions.Add(SwitchCharacter.Who.Partner, new List<GameObject>());
    }
    public void EnablePossessions(SwitchCharacter.Who who) {
        if (who == SwitchCharacter.Who.Kizuna) {
            foreach (GameObject obj in possessions[SwitchCharacter.Who.Kizuna]) {
                obj.SetActive(true);;
            }

            foreach (GameObject obj in possessions[SwitchCharacter.Who.Partner]) {
                obj.SetActive(false);;
            }
        } else {
            foreach (GameObject obj in possessions[SwitchCharacter.Who.Kizuna]) {
                obj.SetActive(false);;
            }

            foreach (GameObject obj in possessions[SwitchCharacter.Who.Partner]) {
                obj.SetActive(true);;
            }
        }
    }

    public void AddPossession(SwitchCharacter.Who who, GameObject obj) {
        possessions[who].Add(obj);
    }

     public void RemovePossession(SwitchCharacter.Who who, GameObject obj) {
        possessions[who].Remove(obj);
    }
}
