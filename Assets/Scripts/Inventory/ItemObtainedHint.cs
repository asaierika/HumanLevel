using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObtainedHint : MonoBehaviour
{
    public static ItemObtainedHint instance;
    public static bool isActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject hintBox;
    public Transform hintBoxTransform;
    public Image itemImage;
    public Text name;
    public List<Item> items;

    private void Start()
    {
    }

    private void Update() {
        /*
        if (isActive && !DialogueManager.inDialogue)
        {
            isActive = false;
            StartCoroutine(Display());
        }
        */

        if (items.Count != 0 && isActive && !DialogueManager.inDialogue)
        {
            isActive = false;
            StartCoroutine(Display());
        }
    }

    public void Show(Item item)
    {
        isActive = true;
        items.Add(item);
        //itemImage.sprite = item.itemImage;
        //name.text = item.nameOfItem;
    }

    IEnumerator Display()
    {
        
        hintBox.SetActive(true);
        
        for (int j = 0; j < items.Count; j++)
        {
            itemImage.sprite = items[j].itemImage;
            name.text = items[j].nameOfItem;
        
            for (float i = 0; i < 840; i += 40f)
            {
                hintBoxTransform.position = new Vector3(hintBoxTransform.position.x - 40, 
                        hintBoxTransform.position.y, hintBoxTransform.position.z);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(2f);

            for (float i = 0; i < 840; i += 40f)
            {
                hintBoxTransform.position = new Vector3(hintBoxTransform.position.x + 40, 
                        hintBoxTransform.position.y, hintBoxTransform.position.z);
                yield return new WaitForEndOfFrame();
            }
        }
        hintBox.SetActive(false);
        items.Clear();
    }
}
