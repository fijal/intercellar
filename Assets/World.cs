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
    public GameObject selected;

    HashSet<Bubble> bubbles = new HashSet<Bubble>();
    HashSet<Spring> springs = new HashSet<Spring>();

    public void Start()
    {
        /*var bub1 = spawnBubble(0, 0);
        var bub2 = spawnBubble(-2, -1);
        var bub3 = spawnBubble(-2, 3);
        var bub4 = spawnBubble(2, 3);
        createSpring(bub1, bub2);
        createSpring(bub2, bub3);
        createSpring(bub3, bub4);
        createSpring(bub4, bub1);*/
    }

    public void Update()
    {
        if (UnityEngine.Random.Range(0, 1f) < 0.3f * Time.deltaTime)
        {
            var bub = spawnBubble(18f, UnityEngine.Random.Range(-5f, 5f));
            bub.velocity += new Vector2(UnityEngine.Random.Range(-0.2f, 0f), UnityEngine.Random.Range(-0.1f, 0.1f));
        }
    }

    public void setPulled(GameObject obj)
    {
        selected = obj;
    }

    public Bubble spawnBubble(float x, float y)
    {
        var obj = Instantiate(bubblePrefab, new Vector3(0, 0, -1), Quaternion.identity, transform);
        obj.transform.localPosition = new Vector3(x, y, -1);
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

    public void removeBubble(Bubble bubble)
    {
        // XXX this is a hack to find the right spring, probably some bookkeeping would be better
        HashSet<Spring> ss = new HashSet<Spring>();
        HashSet<Bubble> neighbours = new HashSet<Bubble>();
        foreachSpring(s =>
        {
            if (s.start == bubble)
            {
                ss.Add(s);
                neighbours.Add(s.end);
            } else if (s.end == bubble)
            {
                ss.Add(s);
                neighbours.Add(s.start);
            }
        });

        foreach (var s in ss)
        {
            springs.Remove(s);
            Destroy(s.gameObject);
        }

        bubbles.Remove(bubble);
        Destroy(bubble.gameObject);

        if (neighbours.Count == 1)
            return; // we don't need to create anything just yet
        Debug.Assert(neighbours.Count == 2);
        Bubble b1 = null, b2 = null;
        foreach (var n in neighbours)
        {
            if (b1 == null)
                b1 = n;
            else if (b2 == null)
                b2 = n;
        }
        var spr = Instantiate(springPrefab, new Vector3(0, 0, -1), Quaternion.identity, transform).GetComponent<Spring>();
        spr.start = b1;
        spr.end = b2;
        springs.Add(spr);
        spr.adjustPosition();
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
