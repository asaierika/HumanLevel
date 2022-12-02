using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            hits[i] = null;
        }
    }

    protected abstract void OnCollide(Collider2D coll);
}