using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2 : MonoBehaviour
{
    public World world;
    public Collider2D backgroundCollider;
    public GameObject smudgePrefab;

    public const float VISCOSITY = 0.95f;
    public const float MAX_STREAM_FORCE = 0.03f;
    public const float MAX_STREAM_RANGE = 4f;

    private void Start()
    {
    }
    
    void FixedUpdate()
    {
        // NOTE: this is apparently necessary for using colliders here
        Physics2D.SyncTransforms();

        // start force calculation
        world.foreachBubble(x => {
            x.force = Vector2.zero;
        });
        world.foreachSpring(x => { x.calculateForces(); });
        DetectBubblesOnBackground();
        world.foreachBubble(x => { x.addFlowForce();  });
        if (world.selected)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            var vec = pos - (Vector2)(world.selected.transform.position);
            world.selected.GetComponent<Bubble>().applyForce(vec.magnitude * 0.1f, vec);
        }

        // done applying forces

        world.foreachBubble(bub =>
        {
            var hit = Physics2D.OverlapCircle(bub.transform.position, 0.7f * transform.localScale.x);
            if (hit && hit.gameObject != bub.gameObject && hit.gameObject.GetComponent<Bubble>())
            {
                if (((Vector2)(hit.transform.position - bub.transform.position)).magnitude > 1.0f)
                {
                    Debug.Log(string.Format("scale: {0}", transform.localScale.x * 0.7f));
                    Debug.Log(hit.gameObject);
                    Debug.Log(bub.gameObject);
                    Debug.Log(hit.gameObject.transform.position);
                    Debug.Log(bub.gameObject.transform.position);
                }
                world.createSpring(hit.gameObject.GetComponent<Bubble>(), bub);
            }
        });

        world.foreachBubble(x => { x.move(); });
        world.foreachSpring(x => { x.adjustPosition(); });
        var vol = ComputeInternalVolume();
        var center = ComputeCenter();
    }

    void DetectBubblesOnBackground()
    {
        var results = new List<Collider2D>();
        backgroundCollider.OverlapCollider(new ContactFilter2D(), results);
        foreach (var coll in results)
        {
            var bubble = coll.GetComponent<Bubble>();
            if (bubble != null)
                bubble.onGround = true;
        }
    }

    public float ComputeInternalVolume()
    {
        return 0f;
        /*
        /* XXX assumes that the bubbles are listed in order, around the single cell 
        float area_times_2 = 0;
        Vector2 p1 = bubbles[bubbles.Length - 1].transform.localPosition;
        for (int i = 0; i < bubbles.Length; i++)
        {
            Vector2 p2 = bubbles[i].transform.localPosition;
            float s1 = p1.x;
            float t1 = p1.y;
            float s2 = p2.x;
            float t2 = p2.y;
            area_times_2 += (s1 - s2) * (t1 + t2);
            p1 = p2;
        }
        return Mathf.Abs(area_times_2 * 0.5f);*/
    }

    public Vector2 ComputeCenter()
    {
        /*Vector2 center = new Vector2(0, 0);
        foreach (var bubble in bubbles)
            center += (Vector2)bubble.transform.position;
        center /= bubbles.Length;
        return center;*/
        return Vector2.zero;
    }
}
