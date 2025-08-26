using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNode : BT.ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        AIAgent ai = black_board.runner.GetComponent<AIAgent>();
        ai.StopMove();
    }

    protected override State OnUpdate()
    {
        AIAgent ai = black_board.runner.GetComponent<AIAgent>();
        ai.MoveTo(black_board.player.transform.position);
        return State.Running;
    }
}
