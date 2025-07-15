using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class World : MonoBehaviour
{
    public Physics2 physics; // XXX a bit of a mess, but there is mostly singletons around

    public GameObject bubblePrefab;
    public GameObject springPrefab;

    HashSet<Bubble> bubbles = new HashSet<Bubble>();
    HashSet<Spring> springs = new HashSet<Spring>();

    public void Start()
    {
        var bub1 = spawnBubble(0, 0);
        var bub2 = spawnBubble(-2, -1);
        var bub3 = spawnBubble(-2, 3);
        createSpring(bub1, bub2);
        createSpring(bub2, bub3);
    }

    public Bubble spawnBubble(float x, float y)
    {
        var obj = Instantiate(bubblePrefab, new Vector3(x, y, -1), Quaternion.identity, transform);
        var bub = obj.GetComponent<Bubble>();
        bubbles.Add(bub);
        return bub;
    }

    public Spring createSpring(Bubble start, Bubble end)
    {
        var center = (end.transform.position - start.transform.position);
        var obj = Instantiate(springPrefab, new Vector3(center.x, center.y, -1), Quaternion.identity, transform);
        var spring = obj.GetComponent<Spring>();
        spring.start = start;
        spring.end = end;
        spring.adjustPosition(); // make sure we don't have a frame with a random square
        springs.Add(spring);
        return spring;
    }

    public void addBubbleInSpring(Spring spring, Vector2 hit)
    {
        GameObject bubbleObj = Instantiate(bubblePrefab, new Vector3(hit.x, hit.y, -1), Quaternion.identity, transform);
        Bubble bubble = bubbleObj.GetComponent<Bubble>();
        bubbles.Add(bubble);
        // create two new springs
        var spr1 = Instantiate(springPrefab, new Vector3(0, 0, -1), Quaternion.identity, transform).GetComponent<Spring>();
        spr1.start = spring.start;
        spr1.end = bubble;
        spr1.adjustPosition();
        var spr2 = Instantiate(springPrefab, new Vector3(0, 0, -1), Quaternion.identity, transform).GetComponent<Spring>();
        spr2.start = bubble;
        spr2.end = spring.end;
        spr2.adjustPosition();
        springs.Remove(spring);
        springs.Add(spr1);
        springs.Add(spr2);
        Destroy(spring.gameObject);

    }
    public void foreachBubble(Action<Bubble> func)
    {
        foreach (var b in bubbles)
        {
            func(b);
        }
    }
    public void foreachSpring(Action<Spring> func)
    {
        foreach (var s in springs)
            func(s);
    }

}
