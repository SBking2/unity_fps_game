using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BT
{
    [CreateAssetMenu(menuName = "SO / Behaviour Tree", fileName = "BehaviourTree")]
    public class BehaviourTree : ScriptableObject
    {
        public Node root;
        public State tree_state = State.Running;
        public List<Node> nodes = new List<Node>();
        public BlackBoard black_board = new BlackBoard();
        public State Update()
        {
            if(tree_state == State.Running)
                tree_state = root.Update();

            return tree_state;
        }
        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);     //把子资源添加到主资源中，跟着tree一起被保存
            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
            
            AssetDatabase.SaveAssets();
            return node;
        }
        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");
            nodes.Remove(node);
            //AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }
        public void AddChildren(Node parent, Node child)
        {
            if (parent is DecoratorNode)
            {
                Undo.RecordObject(parent, "Behaviour Tree (Add Children)");
                (parent as DecoratorNode).child = child;
                EditorUtility.SetDirty(parent);
            }
            else if (parent is CompositeNode)
            {
                Undo.RecordObject(parent, "Behaviour Tree (Add Children)");
                (parent as CompositeNode).children.Add(child);
                EditorUtility.SetDirty(parent);
            }
            else if(parent is RootNode)
            {
                Undo.RecordObject(parent, "Behaviour Tree (Add Children)");
                (parent as RootNode).child = child;
                EditorUtility.SetDirty(parent);
            }
        }
        public void RemovecChilden(Node parent, Node child)
        {
            if (parent is DecoratorNode)
            {
                Undo.RecordObject(parent, "Behaviour Tree (Remove Children)");
                (parent as DecoratorNode).child = null;
                EditorUtility.SetDirty(parent);
            }
            else if (parent is RootNode)
            {
                Undo.RecordObject(parent, "Behaviour Tree (Remove Children)");
                (parent as RootNode).child = null;
                EditorUtility.SetDirty(parent);
            }
            else if (parent is CompositeNode)
            {
                Undo.RecordObject(parent, "Behaviour Tree (Remove Children)");
                (parent as CompositeNode).children.Remove(child);
                EditorUtility.SetDirty(parent);
            }
        }
        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            if (parent is DecoratorNode)
            {
                if ((parent as DecoratorNode).child != null)
                {
                    children.Add((parent as DecoratorNode).child);
                }
            }
            else if(parent is RootNode)
            {
                if ((parent as RootNode).child != null)
                {
                    children.Add((parent as RootNode).child);
                }
            }
            else if (parent is CompositeNode)
            {
                children = (parent as CompositeNode).children;
            }

            return children;
        }
        public void Traverse(Node node, System.Action<Node> visiter)
        {
            if(node)
            {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }
        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.root = tree.root.Clone();
            tree.nodes = new List<Node>();
            Traverse(tree.root, (n) =>
            {
                tree.nodes.Add(n);
            });
            tree.Bind();
            return tree;
        }
        public void Bind()
        {
            Traverse(root, (n) =>
            {
                n.black_board = this.black_board;
            });
        }
    }
}
