using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;

    protected override void OnStart()
    {
        Debug.Log(string.Format("OnStart:{0}", message));
    }

    protected override void OnStop()
    {
        Debug.Log(string.Format("OnStop:{0}", message));
    }

    protected override State OnUpdate()
    {
        Debug.Log(string.Format("OnUpdate:{0}", message));
        return State.Success;
    }
}
