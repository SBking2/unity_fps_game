using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistanceNode : BT.ActionNode
{
    public float distance;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        float dis = Vector3.Distance(black_board.player.transform.position
            , black_board.runner.transform.position);
        if (dis < distance)
            return State.Success;
        else
            return State.Failure;
    }
}
