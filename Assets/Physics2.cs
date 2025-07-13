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
        Debug.Log(ComputeInternalVolume());
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

    public float ComputeInternalVolume()
    {
        float area_times_2 = 0;
        Vector2 p1 = springs[springs.Length - 1].end.transform.localPosition;
        for (int i = 0; i < springs.Length; i++)
        {
            Vector2 p2 = springs[i].end.transform.localPosition;
            float s1 = p1.x;
            float t1 = p1.y;
            float s2 = p2.x;
            float t2 = p2.y;
            area_times_2 += (s1 - s2) * (t1 + t2);
            p1 = p2;
        }
        return Mathf.Abs(area_times_2 * 0.5f);
    }
}
