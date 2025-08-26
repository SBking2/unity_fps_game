using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float duration;
    private float start_time;

    protected override void OnStart()
    {
        start_time = Time.time;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (Time.time - start_time < duration)
            return State.Running;
        else
            return State.Success;
    }
}
