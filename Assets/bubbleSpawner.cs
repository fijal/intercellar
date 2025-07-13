using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleSpawner : MonoBehaviour
{
    float[] events = { 0.0f, 1f, 2, 3, 3.5f, 3.7f };
    int curEv = 0;
    public Physics physics;
    GameObject lastBubble;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (curEv == -1)
            return;
        if (Time.time > events[curEv])
        {
            spawnBubble(0, 0);
            curEv += 1;
            if (curEv >= events.Length)
                curEv = -1;
        }
    }

    void spawnBubble(float x, float y)
    {

    }
}
