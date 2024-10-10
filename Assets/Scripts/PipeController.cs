using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public GameObject start, end;

    Vector3 getPipeVector()
    {
        return end.transform.position + end.GetComponent<PhysicsObject>().attachmentPoint -
                    (start.transform.position + start.GetComponent<PhysicsObject>().attachmentPoint);
    }

    public void computeStretch()
    {
        var diff = getPipeVector();
        transform.position = start.transform.position + start.GetComponent<PhysicsObject>().attachmentPoint + (diff) / 2;
        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(diff.magnitude, transform.localScale.y);
    }

    public void computeForces()
    {
        return;
        if (transform.localScale.x == 1f)
            return;
        float forceV;
        if (transform.localScale.x < 1f)
        {
            forceV = -1 / transform.localScale.x;
        } else
        {
            forceV = transform.localScale.x;
        }
        var force = getPipeVector().normalized * forceV;
        start.GetComponent<PhysicsObject>().addForce(force);
        end.GetComponent<PhysicsObject>().addForce(-force);
    }
}
