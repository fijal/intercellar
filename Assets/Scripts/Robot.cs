using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    Action[] actions;
    int currentAction;

    /*xxx

    Vector3 speed = new Vector3(0, 0, 0);
    float acceleration = 0f;

    const float MAX_ACCELERATION = 0.1f;
    const float MAX_SPEED = 1f;
    */
    void Start()
    {
    }

    public void setActions(Action[] actions)
    {
        this.actions = actions;
        currentAction = 0;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (actions.Length == 0)
            return;
        if (actions[currentAction] is Destroy)
        {
            Destroy(this.gameObject);
            return;
        }

        if (actions[currentAction].advance(Time.deltaTime, this))
        {
            currentAction += 1;
            if (currentAction >= actions.Length)
            {
                actions = new Action[0];
                currentAction = 0;
            }
        }
    }
}
