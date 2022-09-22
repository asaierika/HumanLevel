using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    public string sceneName;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    protected override void OnCollide(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
            playerStorage.initialValue = playerPosition;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            
        }
    }
}
