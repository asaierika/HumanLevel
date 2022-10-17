using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public GameEvent startFishing;
    public GameEvent endFishing;
    public SpriteRenderer kizuna;
    public GameObject fishingKizuna;

    // Start is called before the first frame update
    void Update()
    {
         if (!DialogueManager.inDialogue)
        {
            endFishing.TriggerEvent();
        }
    }

    public void changeSpriteToFishing()
    {
        // if don't wait for some time before start fishing,
        // endFishing in FishingArea will be called in the Update
        // method since inDialogue in DialogueManager is also
        // set in a coroutine so the value changes to true after 
        // 0.1f than when the dialogue starts.
        StartCoroutine(changeSpriteToFishingCoroutine());
    }

    public void changeSpriteToNormal()
    {
        fishingKizuna.SetActive(false);
        kizuna.enabled = true;
    }

    IEnumerator changeSpriteToFishingCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        fishingKizuna.SetActive(true);
        kizuna.enabled = false;
    }
    
}
