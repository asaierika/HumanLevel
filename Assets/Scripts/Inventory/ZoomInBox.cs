using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInBox : MonoBehaviour
{
    public static ZoomInBox instance;

    public GameObject zoomInBox;
    public Image itemImage;
    public Text description;

    public void Show(Item item)
    {
        itemImage.sprite = item.itemImage;
        description.text = item.description;

        
        if (zoomInBox.activeInHierarchy)
        {
            zoomInBox.SetActive(false);
        } else
        {
            zoomInBox.SetActive(true);
        }
        
    }
}
