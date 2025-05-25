using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public Bubble start, end;

    const float LENGTH = 4.0f;

    public void calculateForces()
    {
        Vector2 v_along = end.transform.localPosition - start.transform.localPosition;
        float d = Vector2.Distance(start.transform.localPosition, end.transform.localPosition);
        start.applyForce(d - LENGTH, v_along);
        end.applyForce(LENGTH - d, v_along);

        Vector2 v_transverse = new Vector2(v_along.y, -v_along.x);
        start.applyForce(0.04f, v_transverse);
        end.applyForce(0.04f, v_transverse);
    }

    public void adjustPosition()
    {
        var newPos = (Vector2)(end.transform.localPosition + start.transform.localPosition) / 2;
        transform.localPosition = new Vector3(newPos.x, newPos.y, transform.localPosition.z);
        var newLength = Vector2.Distance(end.transform.localPosition, start.transform.localPosition) / 2 - 1;
        var newWidth = 0.1f;
        transform.localScale = new Vector3(newLength, newWidth, 1);
        var d = end.transform.localPosition - start.transform.localPosition;
        var angle = Mathf.Rad2Deg * Mathf.Atan2(d.y, d.x);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
