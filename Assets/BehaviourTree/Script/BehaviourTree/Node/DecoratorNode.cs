using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class DecoratorNode : Node
    {
        public Node child;

        public override Node Clone()
        {
            DecoratorNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

        public override void Abort()
        {
            base.Abort();
            if(child != null)
            {
                child.Abort();
            }
        }
    }
}
