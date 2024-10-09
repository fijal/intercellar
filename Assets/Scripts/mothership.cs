using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    public GameObject starship;
    public GameObject bubble;
    public GameObject asteroid;
    public GameObject[] pipes;
    public Vector3 attachmentPoint;

    const float SPEED = 0.03f;
    const float FUEL_CONSUMPTION = -0.005f;

    Dictionary<KeyCode, Vector3> movement;

    void Start()
    {
        movement = new Dictionary<KeyCode, Vector3>();
        movement.Add(KeyCode.A, new Vector3(-SPEED, 0, 0));
        movement.Add(KeyCode.W, new Vector3(0, SPEED, 0));
        movement.Add(KeyCode.S, new Vector3(0, -SPEED, 0));
        movement.Add(KeyCode.D, new Vector3(SPEED, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ship = Instantiate(starship, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            var actions = new Action[] { new GoTo(asteroid, ship, 0.2f), new Wait(2), new GoTo(bubble, ship, 1f), new Wait(1),
                                         new GrowBubble(bubble), new Destroy() };
            ship.GetComponent<Robot>().setActions(actions);
        }
    }

    void FixedUpdate()
    {
        foreach (var k in movement)
        {
            if (Input.GetKey(k.Key))
            {
                if (bubble.GetComponent<Bubble>().changeLevel(FUEL_CONSUMPTION) > 0)
                {
                    GetComponent<PhysicsObject>().addForce(k.Value);
                }
            }
        }
    }
}
