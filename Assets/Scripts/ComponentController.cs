using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentController : MonoBehaviour
{
    // Update is called once per frame
    public void MovementAbility()
    {
       GetComponent<PlayerMovement>().enabled = false;
    }
}
