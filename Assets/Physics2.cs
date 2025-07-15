using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2 : MonoBehaviour
{
    public World world;
    public Collider2D backgroundCollider;
    
    private void Start()
    {
    //    bubbles = FindObjectsByType<Bubble>(FindObjectsSortMode.None);
    //   springs = FindObjectsByType<Spring>(FindObjectsSortMode.None);
    }

    public void addBubble()
    {
    /*    var idx = Random.Range(0, bubbles.Length);
        var prev = idx - 1;
        if (prev < 0)
            prev = bubbles.Length - 1;
        var oldBubbles = bubbles;
        var newBubbles = new Bubble[bubbles.Length + 1];
        System.Array.Copy(oldBubbles, 0, newBubbles, 0, prev + 1);
        System.Array.Copy(oldBubbles, idx, newBubbles, idx + 1, oldBubbles.Length - idx);
        var position = (Vector2)((bubbles[idx].transform.position + bubbles[prev].transform.position) / 2);
        GameObject bubble = Instantiate(world.bubblePrefab, new Vector3(position.x, position.y, -1), Quaternion.identity, world.transform);
        newBubbles[idx] = bubble.GetComponent<Bubble>();
        
        Spring spring;
        if (oldBubbles[prev].springs[0].start == oldBubbles[idx] || oldBubbles[prev].springs[0].end == oldBubbles[idx])
            spring = oldBubbles[prev].springs[0];
        else
            spring = oldBubbles[prev].springs[1];
        //Debug.Assert((spring.start == oldBubbles[idx] && spring.end == oldBubbles[prev]) ||
        //             (spring.end == oldBubbles[idx] && spring.start == oldBubbles[prev]));
        var spr1 = Instantiate(world.springPrefab, new Vector3(0, 0, -1), Quaternion.identity, world.transform).GetComponent<Spring>();
        spr1.start = oldBubbles[prev];
        spr1.end = newBubbles[idx];
        var spr2 = Instantiate(world.springPrefab, new Vector3(0, 0, -1), Quaternion.identity, world.transform).GetComponent<Spring>();
        spr2.start = newBubbles[idx];
        spr2.end = oldBubbles[idx];
        bubbles[idx].springs = new Spring[2] { spr1, spr2 };
        if (oldBubbles[prev].springs[0] == spring)
            oldBubbles[prev].springs[0] = spr1;
        if (oldBubbles[prev].springs[1] == spring)
            oldBubbles[prev].springs[1] = spr1;
        if (oldBubbles[idx].springs[0] == spring)
            oldBubbles[idx].springs[0] = spr2;
        if (oldBubbles[idx].springs[1] == spring)
            oldBubbles[idx].springs[1] = spr2;
        bubbles = newBubbles;
        var newSprings = new Spring[springs.Length + 1];
        for (var i = 0; i < springs.Length; i++)
        {
            if (springs[i] == spring)
                newSprings[i] = spr1;
            else
                newSprings[i] = springs[i];
        }
        newSprings[newSprings.Length - 1] = spr2;
        springs = newSprings;
        Destroy(spring.gameObject);*/
    }

    public void removeBubble()
    {
        // XXX
    }

    void FixedUpdate()
    {
        world.foreachBubble(x => {
            x.force = Vector2.zero;
        });
        world.foreachSpring(x => { x.calculateForces(); });
        DetectBubblesOnBackground();
        world.foreachBubble(x => { x.move(); });
        world.foreachSpring(x => { x.adjustPosition(); });
        var vol = ComputeInternalVolume();
        var center = ComputeCenter();
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
        return 0f;
        /*
        /* XXX assumes that the bubbles are listed in order, around the single cell 
        float area_times_2 = 0;
        Vector2 p1 = bubbles[bubbles.Length - 1].transform.localPosition;
        for (int i = 0; i < bubbles.Length; i++)
        {
            Vector2 p2 = bubbles[i].transform.localPosition;
            float s1 = p1.x;
            float t1 = p1.y;
            float s2 = p2.x;
            float t2 = p2.y;
            area_times_2 += (s1 - s2) * (t1 + t2);
            p1 = p2;
        }
        return Mathf.Abs(area_times_2 * 0.5f);*/
    }

    public Vector2 ComputeCenter()
    {
        /*Vector2 center = new Vector2(0, 0);
        foreach (var bubble in bubbles)
            center += (Vector2)bubble.transform.position;
        center /= bubbles.Length;
        return center;*/
        return Vector2.zero;
    }
}
