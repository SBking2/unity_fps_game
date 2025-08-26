using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    private int current_index;

    protected override void OnStart()
    {
        current_index = 0;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (children.Count == 0) return State.Failure;

        OnStart();
        while (true)
        {
            State child_state = children[current_index].Update();
            if(child_state != State.Failure)
            {
                for(int i = current_index + 1; i < children.Count; i++)
                {
                    children[i].Abort();    //打断后续结点
                }
                return child_state;
            }

            current_index++;
            if(current_index >= children.Count)
                return State.Failure;
        }
    }
}
