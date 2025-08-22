using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleColliderHandler : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private Transform m_collider_father;

    private List<Transform> m_bones = new List<Transform>();
    private List<Transform> m_binds = new List<Transform>();

    private void Start()
    {
        GetBones();
        GetBinds();
    }

    private void LateUpdate()
    {
        for(int i = 0; i < 14; i++)
        {
            if (m_binds[i] != null && m_bones[i] != null)
            {
                m_binds[i].transform.position = m_bones[i].transform.position;
                m_binds[i].transform.rotation = m_bones[i].transform.rotation;
            }
        }
    }

    private void GetBones()
    {
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.Head));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.LeftUpperArm));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.LeftLowerArm));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.LeftHand));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.RightUpperArm));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.RightLowerArm));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.RightHand));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.Spine));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.LeftFoot));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.RightUpperLeg));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.RightLowerLeg));
        m_bones.Add(m_animator.GetBoneTransform(HumanBodyBones.RightFoot));
    }

    private void GetBinds()
    {
        m_binds.Add(m_collider_father.Find(HumanBodyBones.Head.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.LeftUpperArm.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.LeftLowerArm.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.LeftHand.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.RightUpperArm.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.RightLowerArm.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.RightHand.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.Spine.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.LeftUpperLeg.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.LeftLowerLeg.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.LeftFoot.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.RightUpperLeg.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.RightLowerLeg.ToString()).transform);
        m_binds.Add(m_collider_father.Find(HumanBodyBones.RightFoot.ToString()).transform);
    }
}
