using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    private Vector3 pos;
    public float minX = -2f;
    public float maxX = 5f;
    public float minY = -1f;
    public float maxY = 0f;

    private void Start()
    {
        player = player == null ? GameObject.FindWithTag("Player").transform : player;
    }

    private void LateUpdate()
    {
        pos = transform.position;
        pos.x = player.position.x;
        pos.y = player.position.y;

        if (pos.x < minX)
        {
            pos.x = minX;
        }
        if (pos.x > maxX)
        {
            pos.x = maxX;
        }
       
        if (pos.y < minY)
        {
            pos.y = minY;
        }
        if (pos.y > maxY)
        {
            pos.y = maxY;
        }
       
        //pos.z = -1f; 

        transform.position = pos;
    }

}
