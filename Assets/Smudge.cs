using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smudge : MonoBehaviour
{
    public float distance = 3f;
    public float duration = 0.5f;
    public float shift = 0f; // [0-1]
    Color baseColor;
    float baseX;

    // Start is called before the first frame update
    void Start()
    {
        shift = Random.Range(0f, 1f);
        baseColor = GetComponent<SpriteRenderer>().material.color;
        baseX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        var fi = (((Time.time - shift * Mathf.PI * 2) / duration) % (Mathf.PI * 2));
        var x = baseX + fi * distance / (Mathf.PI * 2);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        var a = (Mathf.Cos(fi + Mathf.PI) + 1) / 2;
        GetComponent<SpriteRenderer>().material.color = new Color(baseColor.r, baseColor.g, baseColor.b, a);
    }
}
