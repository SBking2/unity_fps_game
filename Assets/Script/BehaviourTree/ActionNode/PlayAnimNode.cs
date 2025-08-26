using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimNode : BT.ActionNode
{
    public string anim_name;
    public int anim_layer;
    protected override void OnStart()
    {
        if (black_board.runner == null) return;
        AnimationController ac = black_board.runner.GetComponent<AnimationController>();
        if (ac != null)
        {
            ac.PlayAnim(anim_name, anim_layer, 0.2f, 0.0f);
        }
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
