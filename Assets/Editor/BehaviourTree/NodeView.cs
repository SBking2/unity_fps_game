using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using BT;
using UnityEditor.Experimental.GraphView;
using System;
using UnityEditor;
using UnityEditor.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public BT.Node node;
    public Port input_port;
    public Port output_port;
    public Action<NodeView> onSelect;
    public NodeView(BT.Node node) : base("Assets/Editor/BehaviourTree/NodeView.uxml")
    {
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTree/NodeView.uss");
        styleSheets.Add(styleSheet);

        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;

        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPort();
        SetupNodeColor();

        Label description = this.Q<Label>("description-label");
        description.bindingPath = "description";
        description.Bind(new SerializedObject(node));
    }
    private void SetupNodeColor()
    {
        if (node is ActionNode)
        {
            AddToClassList("action");
        }
        else if (node is CompositeNode)
        {
            AddToClassList("composite");
        }
        else if (node is DecoratorNode)
        {
            AddToClassList("decorator");
        }else
        {
            AddToClassList("root");
        }
    }
    private void CreateInputPorts()
    {
        if(node is ActionNode)
        {
            input_port = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is CompositeNode)
        {
            input_port = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is DecoratorNode)
        {
            input_port = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }

        if(input_port != null)
        {
            input_port.portName = "";
            input_port.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(input_port);
        }
    }
    private void CreateOutputPort()
    {
        if (node is ActionNode)
        {
        }
        else if (node is RootNode)
        {
            output_port = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is CompositeNode)
        {
            output_port = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is DecoratorNode)
        {
            output_port = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }

        if (output_port != null)
        {
            output_port.portName = "";
            output_port.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(output_port);
        }
    }
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Behaviour Tree (Set Position)");
        node.position.x = newPos.x;
        node.position.y = newPos.y;
        EditorUtility.SetDirty(node);
    }
    public override void OnSelected()
    {
        base.OnSelected();
        if(onSelect != null)
        {
            onSelect(this);
        }
    }
    public void SortChildren()
    {
        CompositeNode composite = node as CompositeNode;
        if(composite)
        {
            composite.children.Sort(SortByHorizontalPosition);
        }
    }
    private int SortByHorizontalPosition(BT.Node left, BT.Node right)
    {
        return left.position.x < right.position.x ? -1 : 1;
    }
    public void UpdateNodeState()
    {
        RemoveFromClassList("running");
        RemoveFromClassList("success");
        RemoveFromClassList("failure");

        switch(node.state)
        {
            case State.Running:
                AddToClassList("running");
                break;
            case State.Success:
                AddToClassList("success");
                break;
            case State.Failure:
                AddToClassList("failure");
                break;
        }
    }
}
