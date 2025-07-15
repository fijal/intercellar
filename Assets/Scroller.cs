using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public World world;
    static float SCROLL_SPEED = 4f;
    static float SCALE_FACTOR = 1.1f;

    GameObject lastHighlighted;

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
        /*if (Input.GetKeyDown(KeyCode.P))
            world.addBubble();
        if (Input.GetKeyDown(KeyCode.O))
            world.removeBubble();*/
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        if (lastHighlighted)
        {
            lastHighlighted.transform.GetChild(0).gameObject.SetActive(false);
            lastHighlighted = null;
        }
        var hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit) {
            var bubble = hit.collider.gameObject.GetComponent<Bubble>();
            if (bubble)
            {
                bubble.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                lastHighlighted = bubble.gameObject;
            }
            var spring = hit.collider.gameObject.GetComponent<Spring>();
            if (spring)
            {
                spring.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                lastHighlighted = spring.gameObject;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (lastHighlighted)
            {
                if (lastHighlighted.GetComponent<Bubble>())
                {
                    world.removeBubble(lastHighlighted.GetComponent<Bubble>());
                    //Debug.Log("clicked bubble");
                } else if (lastHighlighted.GetComponent<Spring>())
                {
                    world.addBubbleInSpring(lastHighlighted.GetComponent<Spring>(), hit.centroid);
                }
            }
        }

    }
}
