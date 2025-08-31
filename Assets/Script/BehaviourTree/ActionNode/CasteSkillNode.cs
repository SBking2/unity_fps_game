using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasteSkillNode : BT.ActionNode
{
    public Timeline skill;

    protected override void OnStart()
    {
        SkillController sc = black_board.runner.GetComponent<SkillController>();
        sc.CasteSkill(skill);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
