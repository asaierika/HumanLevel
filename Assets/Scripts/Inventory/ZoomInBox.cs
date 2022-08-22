using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInBox : MonoBehaviour
{
    public static ZoomInBox instance;

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

    public GameObject zoomInBox;
    public Image itemImage;
    public Text discription;

    private void Start()
    {
        zoomInBox.transform.localScale = Vector3.zero;
    }

    public void Show(Item item)
    {
        itemImage.sprite = item.itemImage;
        discription.text = item.discription;

        
        if (zoomInBox.transform.localScale == Vector3.one)
        {
            zoomInBox.transform.localScale = Vector3.zero;
        } else
        {
            zoomInBox.transform.localScale = Vector3.one;
        }
        
    }
}
