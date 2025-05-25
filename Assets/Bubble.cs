using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //public GameObject[] springs;

    public Vector2 force;
    public Vector2 velocity;
    const float mass = 1.0f;

    public void applyForce(float force, Vector2 direction)
    {
        this.force += force * direction.normalized;
    }

    public void move()
    {
        var a = force / mass;
        velocity += a * 0.1f;
        velocity *= 0.95f;
        transform.localPosition += new Vector3(velocity.x, velocity.y, 0);
    }
}
