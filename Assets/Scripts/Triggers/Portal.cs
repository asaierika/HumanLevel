using UnityEngine;

public class Portal : Collidable
{
    public string sceneName;

    protected override void OnCollide(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
            SceneTransition.SceneTransit(sceneName);
        }
    }
}
