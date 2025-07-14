using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Physics2 physics; // XXX a bit of a mess, but there is mostly singletons around

    public void addBubble()
    {
        physics.addBubble();
    }

    public void removeBubble()
    {
        physics.removeBubble();
    }
}
