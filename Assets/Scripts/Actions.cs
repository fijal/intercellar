using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Action
{
    public Action()
    {
    }

    public virtual bool advance(float time, Robot robot)
    {
        return true;
    }
}

public class Wait : Action
{
    float left;

    public Wait(float time)
    {
        left = time;
    }

    public override bool advance(float time, Robot robot)
    {
        left -= time;
        if (left <= 0)
            return true;
        return false;
    }
}

public class GoTo : Action
{
    GameObject dest;
    float speed;
    float distance;

    const float MAX_SPEED = 0.03f;

    public GoTo(GameObject destination, GameObject ship, float distance = 0)
    {
        dest = destination;
        this.distance = distance;
        speed = MAX_SPEED;
    }

    public override bool advance(float time, Robot robot)
    {
        if ((robot.transform.position - dest.transform.position).magnitude < speed * 1.1 + distance)
            return true;

        var d = dest.transform.position - robot.transform.position;
        var angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        robot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        var dir = (dest.transform.position - robot.transform.position).normalized * speed;
        robot.transform.position += dir;
        return false;
    }
}

public class GrowBubble : Action
{
    GameObject bubble;

    public GrowBubble(GameObject bubble)
    {
        this.bubble = bubble;
    }

    public override bool advance(float time, Robot robot)
    {
        bubble.GetComponent<Bubble>().changeLevel(0.1f);
        return true;
    }
}

public class Destroy : Action
{
}
