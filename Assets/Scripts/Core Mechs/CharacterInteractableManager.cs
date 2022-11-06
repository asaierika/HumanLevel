using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractableManager : MonoBehaviour
{
    public Dictionary<SwitchCharacter.Who, HashSet<MiniGame>> possessions = new Dictionary<SwitchCharacter.Who, HashSet<MiniGame>>();

    void Awake() {
        possessions.Add(SwitchCharacter.Who.Kizuna, new HashSet<MiniGame>());
        possessions.Add(SwitchCharacter.Who.Partner, new HashSet<MiniGame>());
    }
    public void EnablePossessions() {
        if (SwitchCharacter.who == SwitchCharacter.Who.Kizuna) {
            foreach (MiniGame game in possessions[SwitchCharacter.Who.Kizuna]) {
                game.isCorrectCharacter = true;
                foreach (SpriteRenderer renderer in game.renderers) {
                    renderer.enabled = true;
                }
            }

            foreach (MiniGame game in possessions[SwitchCharacter.Who.Partner]) {
                game.isCorrectCharacter = false;
                foreach (SpriteRenderer renderer in game.renderers) {
                    renderer.enabled = false;
                }
            }
        } else {
            foreach (MiniGame game in possessions[SwitchCharacter.Who.Kizuna]) {
                game.isCorrectCharacter = false;
                foreach (SpriteRenderer renderer in game.renderers) {
                    renderer.enabled = false;
                }
            }

            foreach (MiniGame game in possessions[SwitchCharacter.Who.Partner]) {
                game.isCorrectCharacter = true;
                foreach (SpriteRenderer renderer in game.renderers) {
                    renderer.enabled = true;
                }
            }
        }
    }

    public void AddPossession(SwitchCharacter.Who who, MiniGame game) {
        possessions[who].Add(game);
    }

     public void RemovePossession(SwitchCharacter.Who who, MiniGame game) {
        possessions[who].Remove(game);
    }
}
