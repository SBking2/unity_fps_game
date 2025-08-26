using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using BT;
using System.Linq;
using System;

public class BehaviourTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }

    private BehaviourTree m_tree;
    public System.Action<NodeView> onNodeSelected;
    public BehaviourTreeView()
    {
        Insert(0, new GridBackground());
        
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var style_sheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uss");
        styleSheets.Add(style_sheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }
    private void OnUndoRedo()
    {
        PopulateView(m_tree);
        AssetDatabase.SaveAssets();
    }
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach(var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) =>
                {
                    CreateNode(type);
                });
            }
        }

        {
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) =>
                {
                    CreateNode(type);
                });
            }
        }

        {
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) =>
                {
                    CreateNode(type);
                });
            }
        }
    }
    public void PopulateView(BehaviourTree tree)
    {
        this.m_tree = tree;

        //删除原来的View
        graphViewChanged -= OnGraphViewChnaged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChnaged;

        if (tree == null) return;

        if (m_tree.root == null)
        {
            m_tree.root = m_tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(m_tree);
            AssetDatabase.SaveAssets();
        }

        //更新为新的view
        tree.nodes.ForEach(node => CreateNodeView(node));
        tree.nodes.ForEach(n =>
        {
            var children = m_tree.GetChildren(n);
            children.ForEach(child =>
            {
                NodeView parentView = GetNodeByGuid(n.guid) as NodeView;
                NodeView childView = GetNodeByGuid(child.guid) as NodeView;

                Edge edge = parentView.output_port.ConnectTo(childView.input_port);
                AddElement(edge);
            });
        });
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => 
            endPort.direction != startPort.direction
            && endPort.node != startPort.node
        ).ToList();
    }
    private GraphViewChange OnGraphViewChnaged(GraphViewChange change)
    {
        //移除element
        if(change.elementsToRemove != null)
        {
            change.elementsToRemove.ForEach(elem =>
            {
                NodeView view = elem as NodeView;
                if(view != null)
                {
                    m_tree.DeleteNode(view.node);
                }

                Edge edge = elem as Edge;
                if(edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childenView = edge.input.node as NodeView;
                    m_tree.RemovecChilden(parentView.node, childenView.node);
                }
            });
        }

        //边创建事件
        if(change.edgesToCreate != null)
        {
            change.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childenView = edge.input.node as NodeView;
                m_tree.AddChildren(parentView.node, childenView.node);
            });
        }

        if(change.movedElements != null)
        {
            nodes.ForEach((n) =>
            {
                NodeView view = n as NodeView;
                if(view != null)
                {
                    view.SortChildren();
                }
            });
        }

        return change;
    }
    void CreateNode(System.Type type)
    {
        if(Application.isPlaying)
        {
            Debug.Log("You can't create node in application running!");
            return;
        }

        BT.Node node = m_tree.CreateNode(type);
        CreateNodeView(node);
    }
    void CreateNodeView(BT.Node node)
    {
        NodeView node_view = new NodeView(node);
        node_view.onSelect = onNodeSelected;
        AddElement(node_view);
    }
    public void UpdateNodeState()
    {
        nodes.ForEach((n) =>
        {
            NodeView view = n as NodeView;
            if(view != null)
            {
                view.UpdateNodeState();
            }
        });
    }
}
