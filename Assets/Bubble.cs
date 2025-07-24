using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Vector2 force;
    public Vector2 velocity;
    public bool onGround;
    public bool fixedGround;
    const float mass = 1.0f;
    //public Spring[] springs;

    private void Start()
    {
        /*var my_coll = gameObject.AddComponent<CircleCollider2D>();
        my_coll.radius = 0.93f;*/
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void applyForce(float force, Vector2 direction)
    {
        this.force += force * direction.normalized;
    }

    public void addFlowForce()
    {
        // the stream flows with max speed of MAX_STREAM_FORCE in the middle of the channel,
        // while dropping linearly to 0 at MAX_STREAM_RANGE
        float value;
        value = Mathf.Max(Physics2.MAX_STREAM_RANGE - Mathf.Abs(transform.localPosition.y), 0) / Physics2.MAX_STREAM_RANGE * Physics2.MAX_STREAM_FORCE;
        applyForce(value, new Vector2(-1f, -0));
    }

    public void move()
    {
        if (fixedGround)
            return;
        var a = force / mass;
        velocity += a * 0.1f;
        //velocity += new Vector2(0, onGround ? 0.010f : -0.002f); // XXX disable gravity
        velocity *= Physics2.VISCOSITY;
        transform.localPosition += new Vector3(velocity.x, velocity.y, 0);
        
        onGround = false;
    }
}
