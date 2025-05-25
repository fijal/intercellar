using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2 : MonoBehaviour
{
    public Bubble[] bubbles;
    public Spring[] springs;
    
    private void Start()
    {
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
        foreach (var bubble in bubbles)
        {
            bubble.move();
        }
        foreach (var spring in springs)
        {
            spring.adjustPosition();
        }
    }
}
