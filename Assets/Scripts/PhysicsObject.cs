using System;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float mass = 1f;
    public float resistance = 0.01f;
    const float MIN_SPEED = 0.01f;

    public GameObject pipe;
    public Vector3 attachmentPoint;

    Vector3 speed;
    
    public void addForce(Vector3 force)
    {
        speed += force / mass;
    }

    public void computeMovement()
    {
        transform.position += speed;
        speed /= (resistance + 1f);
        //if (speed.magnitude < MIN_SPEED)
        //    speed = new Vector3(0, 0, 0);
    }
}

