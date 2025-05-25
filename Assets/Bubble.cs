using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //public GameObject[] springs;

    public Vector2 force;
    public Vector2 velocity;
    public bool onGround;
    const float mass = 1.0f;

    Collider2D my_collider;
    CircleCollider2D larger_circle_collider;

    private void Start()
    {
        var my_coll = gameObject.AddComponent<CircleCollider2D>();
        my_coll.radius = 0.93f;
        my_collider = my_coll;

        larger_circle_collider = gameObject.AddComponent<CircleCollider2D>();
        larger_circle_collider.isTrigger = true;
        larger_circle_collider.radius = 4f;
    }

    public void applyForce(float force, Vector2 direction)
    {
        this.force += force * direction.normalized;
    }

    public void move()
    {
        var a = force / mass;
        velocity += a * 0.1f;
        velocity += new Vector2(0, onGround ? 0.010f : -0.002f);
        velocity *= 0.95f;
        transform.localPosition += new Vector3(velocity.x, velocity.y, 0);
        
        onGround = false;
    }
}
