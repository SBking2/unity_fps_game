using BT;
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private BehaviourTreeView m_tree_view;
    private InspectorView m_inspector_view;
    IMGUIContainer m_blackboard_container;
    SerializedObject m_serializedObject;
    SerializedProperty m_serializedProperty;

    [MenuItem("BehaviourTreeEditor/Editor ...")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if(Selection.activeObject is BehaviourTree)
        {
            OpenWindow();
            return true;
        }
        return false;
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        // Instantiate USS
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        m_tree_view = root.Q<BehaviourTreeView>();
        m_inspector_view = root.Q<InspectorView>();
        m_blackboard_container = root.Q<IMGUIContainer>();

        m_blackboard_container.onGUIHandler = () =>
        {
            if(m_serializedObject != null)
            {
                m_serializedObject.Update();
                EditorGUILayout.PropertyField(m_serializedProperty);
                m_serializedObject.ApplyModifiedProperties();
            }
        };

        m_tree_view.onNodeSelected = OnNodeSelected;

        OnSelectionChange();
        Application.quitting += OnQuitPlayingMode;
    }
    private void OnQuitPlayingMode()
    {
        m_tree_view.PopulateView(null);
        m_serializedObject = null;
        m_serializedProperty = null;
    }
    private void OnSelectionChange()
    {
        if(!Selection.activeObject)
        {
            return;
        }

        BehaviourTree tree;

        tree = Selection.activeObject as BehaviourTree;
        if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
        {
            m_tree_view.PopulateView(tree);     //切换行为树
        }
        else if (Application.isPlaying)
        {
            BTRunner runner = Selection.activeObject.GetComponent<BTRunner>();
            if (runner)
            {
                tree = runner.Tree;
                m_tree_view.PopulateView(tree);     //切换行为树
            }

            AIAgent agent = Selection.activeObject.GetComponent<AIAgent>();
            if (agent)
            {
                tree = agent.BT;
                m_tree_view.PopulateView(tree);     //切换行为树
            }
        }

        if(tree != null)
        {
            m_serializedObject = new SerializedObject(tree);
            m_serializedProperty = m_serializedObject.FindProperty("black_board");
        }
        
    }
    private void OnNodeSelected(NodeView view)
    {
        m_inspector_view.UpdateSelector(view);
    }
    private void OnInspectorUpdate()
    {
        if(m_tree_view != null && Application.isPlaying)
        {
            m_tree_view.UpdateNodeState();
        }
    }
}
