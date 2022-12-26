using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to keep track of who interacted with which interactable to allow player to resume their interaction even when they do a 
// character switch.
public class CharacterInteractableManager : MonoBehaviour
{
    // public Dictionary<CharacterSwitcher.Who, HashSet<MiniGame>> possessions = new Dictionary<CharacterSwitcher.Who, HashSet<MiniGame>>();

    // void Awake() {
    //     possessions.Add(CharacterSwitcher.Who.Kizuna, new HashSet<MiniGame>());
    //     possessions.Add(CharacterSwitcher.Who.Partner, new HashSet<MiniGame>());
    // }
    // public void EnablePossessions() {
    //     if (CharacterSwitcher.instance.identity == CharacterSwitcher.Who.Kizuna) {
    //         foreach (MiniGame game in possessions[CharacterSwitcher.Who.Kizuna]) {
    //             game.isCorrectCharacter = true;
    //             foreach (SpriteRenderer renderer in game.renderers) {
    //                 renderer.enabled = true;
    //             }
    //         }

    //         foreach (MiniGame game in possessions[CharacterSwitcher.Who.Partner]) {
    //             game.isCorrectCharacter = false;
    //             foreach (SpriteRenderer renderer in game.renderers) {
    //                 renderer.enabled = false;
    //             }
    //         }
    //     } else {
    //         foreach (MiniGame game in possessions[CharacterSwitcher.Who.Kizuna]) {
    //             game.isCorrectCharacter = false;
    //             foreach (SpriteRenderer renderer in game.renderers) {
    //                 renderer.enabled = false;
    //             }
    //         }

    //         foreach (MiniGame game in possessions[CharacterSwitcher.Who.Partner]) {
    //             game.isCorrectCharacter = true;
    //             foreach (SpriteRenderer renderer in game.renderers) {
    //                 renderer.enabled = true;
    //             }
    //         }
    //     }
    // }

    // public void AddPossession(CharacterSwitcher.Who identity, MiniGame game) {
    //     possessions[identity].Add(game);
    // }

    //  public void RemovePossession(CharacterSwitcher.Who identity, MiniGame game) {
    //     possessions[identity].Remove(game);
    // }
}
