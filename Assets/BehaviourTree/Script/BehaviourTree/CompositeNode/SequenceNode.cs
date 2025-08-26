using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    private int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (children.Count == 0) return State.Failure;

        while(true)
        {
            State chidr_state = children[current].Update();

            if (chidr_state != State.Success)
            {
                for(int i = current + 1; i < children.Count; i++)
                {
                    children[i].Abort();
                }
                return chidr_state;
            }  

            current++;

            //如果子节点都执行了
            if (current >= children.Count)
                return State.Success;
        }
    }
}
