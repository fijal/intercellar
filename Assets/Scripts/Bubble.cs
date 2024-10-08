using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    const float MIN_SIZE = 0.1f;
    float radius = 1f;

    public float changeLevel(float diff)
    {
        var volume = Mathf.PI * radius * radius + diff;
        if (volume <= 0)
        {
            return 0f;
        }
        radius = Mathf.Sqrt(volume / Mathf.PI);
        transform.localScale = new Vector3(radius, radius);
        return radius;
    }

}
