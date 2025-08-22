using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 负责把transform 和 骨骼一一绑定，轴心对齐
/// </summary>

public class BindBoneWindow : EditorWindow
{
    private static BindBoneWindow s_window = null;

    [MenuItem("Tools/Bind Bone Window")]
    public static void OpenWindow()
    {
        s_window = GetWindow<BindBoneWindow>();
    }

    private List<Transform> m_bones = new List<Transform>();
    private List<Transform> m_binds = new List<Transform>();

    private Animator m_animator;
    private GameObject m_bind_father;

    private void OnEnable()
    {
        for(int i = 0; i < 14; i++)
        {
            m_bones.Add(null);
            m_binds.Add(null);
        }
    }

    private void OnGUI()
    {

        GUILayout.Label("Bind Bone", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Skinned_Mesh_Renderer", GUILayout.Width(150));
        m_animator = (Animator)EditorGUILayout.ObjectField(m_animator
            , typeof(Animator), true, GUILayout.ExpandWidth(false));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bind_Father", GUILayout.Width(150));
        m_bind_father = (GameObject)EditorGUILayout.ObjectField(m_bind_father
            , typeof(GameObject), true, GUILayout.ExpandWidth(false));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        
        EditorGUILayout.BeginVertical();
        DrawOneObjectFields("head_bone", m_bones, 0);
        DrawOneObjectFields("left_uppper_arm_bone", m_bones, 1);
        DrawOneObjectFields("left_lower_arm_bone", m_bones, 2);
        DrawOneObjectFields("left_hand_bone", m_bones, 3);
        DrawOneObjectFields("right_uppper_arm_bone", m_bones, 4);
        DrawOneObjectFields("right_lower_arm_bone", m_bones, 5);
        DrawOneObjectFields("right_hand_bone", m_bones, 6);
        DrawOneObjectFields("body_bone", m_bones, 7);
        DrawOneObjectFields("left_upper_leg_bone", m_bones, 8);
        DrawOneObjectFields("left_lower_leg_bone", m_bones, 9);
        DrawOneObjectFields("left_foot_bone", m_bones, 10);
        DrawOneObjectFields("right_upper_leg_bone", m_bones, 11);
        DrawOneObjectFields("right_lower_leg_bone", m_bones, 12);
        DrawOneObjectFields("right_foot_bone", m_bones, 13);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical();
        DrawOneObjectFields("head_bind", m_binds, 0);
        DrawOneObjectFields("left_uppper_arm_bind", m_binds, 1);
        DrawOneObjectFields("left_lower_arm_bind", m_binds, 2);
        DrawOneObjectFields("left_hand_bind", m_binds, 3);
        DrawOneObjectFields("right_uppper_arm_bind", m_binds, 4);
        DrawOneObjectFields("right_lower_arm_bind", m_binds, 5);
        DrawOneObjectFields("right_hand_bind", m_binds, 6);
        DrawOneObjectFields("body_bind", m_binds, 7);
        DrawOneObjectFields("left_upper_leg_bind", m_binds, 8);
        DrawOneObjectFields("left_lower_leg_bind", m_binds, 9);
        DrawOneObjectFields("left_foot_bind", m_binds, 10);
        DrawOneObjectFields("right_upper_leg_bind", m_binds, 11);
        DrawOneObjectFields("right_lower_leg_bind", m_binds, 12);
        DrawOneObjectFields("right_foot_bind", m_binds, 13);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        if (GUILayout.Button("Auto Select Bone", GUILayout.Width(150)))
        {
            AutoSelectBone();
        }

        if (GUILayout.Button("Auto Select Bind", GUILayout.Width(150)))
        {
            AutoSelectBind();
        }

        if (GUILayout.Button("Bind", GUILayout.Width(150)))
        {
            Bind();
        }
    }

    private void DrawOneObjectFields(string name, List<Transform> objs, int index)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(name, GUILayout.Width(150));
        objs[index] = (Transform)EditorGUILayout.ObjectField(objs[index], typeof(Transform), true, GUILayout.ExpandWidth(false));
        EditorGUILayout.EndHorizontal();
    }
    private void Bind()
    {
        for(int i = 0; i < m_bones.Count; i++)
        {
            if (m_bones[i] != null && m_binds[i] != null)
            {
                m_binds[i].position = m_bones[i].position;
                m_binds[i].rotation = m_bones[i].rotation;
            }
        }
    }

    private void AutoSelectBone()
    {
        if(m_animator == null)
        {
            Debug.LogError("Animator is null!");
            return;
        }

        m_bones[0] = m_animator.GetBoneTransform(HumanBodyBones.Head);
        m_bones[1] = m_animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        m_bones[2] = m_animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        m_bones[3] = m_animator.GetBoneTransform(HumanBodyBones.LeftHand);
        m_bones[4] = m_animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
        m_bones[5] = m_animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
        m_bones[6] = m_animator.GetBoneTransform(HumanBodyBones.RightHand);
        m_bones[7] = m_animator.GetBoneTransform(HumanBodyBones.Spine);
        m_bones[8] = m_animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        m_bones[9] = m_animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        m_bones[10] = m_animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        m_bones[11] = m_animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        m_bones[12] = m_animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        m_bones[13] = m_animator.GetBoneTransform(HumanBodyBones.RightFoot);
    }
    private void AutoSelectBind()
    {
        if (m_bind_father == null)
        {
            Debug.LogError("Bind father is null!");
            return;
        }

        m_binds[0] = GetOneBind(HumanBodyBones.Head.ToString());
        m_binds[1] = GetOneBind((HumanBodyBones.LeftUpperArm.ToString()));
        m_binds[2] = GetOneBind((HumanBodyBones.LeftLowerArm.ToString()));
        m_binds[3] = GetOneBind((HumanBodyBones.LeftHand.ToString()));
        m_binds[4] = GetOneBind((HumanBodyBones.RightUpperArm.ToString()));
        m_binds[5] = GetOneBind((HumanBodyBones.RightLowerArm.ToString()));
        m_binds[6] = GetOneBind((HumanBodyBones.RightHand.ToString()));
        m_binds[7] = GetOneBind((HumanBodyBones.Spine.ToString()));
        m_binds[8] = GetOneBind((HumanBodyBones.LeftUpperLeg.ToString()));
        m_binds[9] = GetOneBind((HumanBodyBones.LeftLowerLeg.ToString()));
        m_binds[10]= GetOneBind((HumanBodyBones.LeftFoot.ToString()));
        m_binds[11]= GetOneBind((HumanBodyBones.RightUpperLeg.ToString()));
        m_binds[12]= GetOneBind((HumanBodyBones.RightLowerLeg.ToString()));
        m_binds[13]= GetOneBind((HumanBodyBones.RightFoot.ToString()));
    }

    /// <summary>
    /// 如果不存在，就自己创建并添加
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private Transform GetOneBind(string name)
    {
        Transform trans = m_bind_father.transform.Find(name);

        if (trans == null)
        {
            GameObject obj = new GameObject(name);
            GameObject collider = new GameObject("Collider");
            collider.transform.SetParent(obj.transform, false);
            collider.AddComponent<CapsuleCollider>();
            trans = obj.transform;
            obj.transform.SetParent(m_bind_father.transform, false);
        }

        return trans;
    }
}
