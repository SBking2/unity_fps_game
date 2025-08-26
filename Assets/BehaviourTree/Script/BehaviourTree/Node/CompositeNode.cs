using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BT
{
    public abstract class CompositeNode : Node
    {
        public List<Node> children = new List<Node>();

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }

        public override void Abort()
        {
            base.Abort();
            foreach(var child in children)
            {
                child.Abort();
            }
        }
    }
}
