using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2 : MonoBehaviour
{
    /*public*/ Bubble[] bubbles;
    /*public*/ Spring[] springs;
    public Collider2D backgroundCollider;
    
    private void Start()
    {
        bubbles = FindObjectsByType<Bubble>(FindObjectsSortMode.None);
        springs = FindObjectsByType<Spring>(FindObjectsSortMode.None);
    }

    void FixedUpdate()
    {
        foreach (var bubble in bubbles)
        {
            bubble.force = Vector2.zero;
        }
        foreach (var spring in springs)
        {
            spring.calculateForces();
        }
        DetectBubblesOnBackground();
        foreach (var bubble in bubbles)
        {
            bubble.move();
        }
        foreach (var spring in springs)
        {
            spring.adjustPosition();
        }
    }

    void DetectBubblesOnBackground()
    {
        var results = new List<Collider2D>();
        backgroundCollider.OverlapCollider(new ContactFilter2D(), results);
        foreach (var coll in results)
        {
            var bubble = coll.GetComponent<Bubble>();
            if (bubble != null)
                bubble.onGround = true;
        }
    }
}
