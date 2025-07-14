using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public World world;
    static float SCROLL_SPEED = 4f;
    static float SCALE_FACTOR = 1.1f;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            world.transform.localPosition += new Vector3(SCROLL_SPEED * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.D))
            world.transform.localPosition += new Vector3(-SCROLL_SPEED * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.W))
            world.transform.localPosition += new Vector3(0, -SCROLL_SPEED * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            world.transform.localPosition += new Vector3(0, SCROLL_SPEED * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
            world.transform.localScale *= (1 + SCALE_FACTOR * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            world.transform.localScale *= 1/(1 + (SCALE_FACTOR * Time.deltaTime));
        if (Input.GetKeyDown(KeyCode.P))
            world.addBubble();
        if (Input.GetKeyDown(KeyCode.O))
            world.removeBubble();
    }
}
