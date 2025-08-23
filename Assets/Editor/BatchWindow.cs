using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BatchWindow : EditorWindow
{
    private static BatchWindow s_window = null;

    [MenuItem("Tools/Batch Window")]
    public static void OpenWindow()
    {
        s_window = GetWindow<BatchWindow>();
    }

    private Transform m_father;
    private List<Collider> m_colliders = new List<Collider>();

    private void OnGUI()
    {
        GUILayout.Label("Batch Collider", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Father", GUILayout.Width(150));
        m_father = (Transform) EditorGUILayout.ObjectField(m_father, typeof(Transform), true);
        EditorGUILayout.EndHorizontal();

        for(int i = 0; i < m_colliders.Count; i++)
        {
            DrawOneCollider(i);
        }

        EditorGUILayout.BeginVertical();
        EditorGUILayout.EndVertical();


        if(GUILayout.Button("Select Collider", GUILayout.Width(150), GUILayout.Height(25)))
        {
            AutoSelectCollider();
            Debug.Log("Select Collider!");
        }

        if(GUILayout.Button("Set Collider", GUILayout.Width(150), GUILayout.Height(25)))
        {
            SetTriggerOrCollider(false);
            Debug.Log("已将碰撞体设置为Collider");
        }

        if (GUILayout.Button("Set Trigger", GUILayout.Width(150), GUILayout.Height(25)))
        {
            SetTriggerOrCollider(true);
            Debug.Log("已将碰撞体设置为Trigger");
        }
    }

    private void DrawOneCollider(int index)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(index.ToString(), GUILayout.Width(150));
        EditorGUILayout.ObjectField(m_colliders[index], typeof(Collider), true);
        EditorGUILayout.EndHorizontal();
    }

    private void AutoSelectCollider()
    {
        if (m_father == null)
        {
            Debug.LogError("Father is Null !");
            return;
        }

        m_colliders.Clear();
        Collider[] colliders = m_father.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            m_colliders.Add(collider);
        }
    }
    
    private void SetTriggerOrCollider(bool isTrigger)
    {
        foreach(var collider in m_colliders)
        {
            collider.isTrigger = isTrigger;
        }
    }
}
