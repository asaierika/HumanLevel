using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Remove this class/
public class CameraMovement : MonoBehaviour
{
    public float[] positionLimits; // Top, down, right, left
    public Vector2 offset; // x, y
    public const int zPos = -1;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        bool inXBoundary = transform.position.x >= positionLimits[3] && transform.position.x <= positionLimits[2];
        bool inYBoundary = transform.position.y <= positionLimits[0] && transform.position.y >= positionLimits[1];
        if (inXBoundary) {
            Debug.Log("Camera within x confine");
            
            float newX = player.position.x + offset.x;
            newX = newX > positionLimits[2] 
            ? positionLimits[2] 
            : newX < positionLimits[3]
            ? positionLimits[3]
            : newX;

            if (inYBoundary) {
                float newY = player.position.y + offset.y;
                newY = newY > positionLimits[0] 
                ? positionLimits[0] 
                : newY < positionLimits[1]
                ? positionLimits[1]
                : newY;

                transform.position = new Vector3(newX, newY, zPos);
            } else {
                transform.position = new Vector3(newX, transform.position.y, zPos);
            }
        } else if (inYBoundary) {
            float newY = player.position.y + offset.y;
            newY = newY > positionLimits[0] 
            ? positionLimits[0] 
            : newY < positionLimits[1]
            ? positionLimits[1]
            : newY;

            transform.position = new Vector3(transform.position.x, newY, zPos);
        }
    }
}
