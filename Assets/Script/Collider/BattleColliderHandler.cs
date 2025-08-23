using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleColliderHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_owner;
    [SerializeField] private Animator m_animator;
    [SerializeField] private BoneHit m_bone_hit;
    [SerializeField] private Transform m_collider_father;

    private List<Transform> m_bones = new List<Transform>();
    private List<Transform> m_binds = new List<Transform>();
    private List<HumanBodyBones> m_bone_enum = new List<HumanBodyBones>()
    {
        HumanBodyBones.Head,
        HumanBodyBones.LeftUpperArm,
        HumanBodyBones.LeftLowerArm,
        HumanBodyBones.LeftHand,
        HumanBodyBones.RightUpperArm,
        HumanBodyBones.RightLowerArm,
        HumanBodyBones.RightHand,
        HumanBodyBones.Spine,
        HumanBodyBones.LeftUpperLeg,
        HumanBodyBones.LeftLowerLeg,
        HumanBodyBones.LeftFoot,
        HumanBodyBones.RightUpperLeg,
        HumanBodyBones.RightLowerLeg,
        HumanBodyBones.RightFoot
    };

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
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[0]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[1]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[2]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[3]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[4]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[5]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[6]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[7]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[8]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[9]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[10]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[11]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[12]));
        m_bones.Add(m_animator.GetBoneTransform(m_bone_enum[13]));
    }

    private void GetBinds()
    {
        m_binds.Add(m_collider_father.Find(m_bone_enum[0].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[1].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[2].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[3].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[4].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[5].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[6].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[7].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[8].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[9].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[10].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[11].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[12].ToString()).transform);
        m_binds.Add(m_collider_father.Find(m_bone_enum[13].ToString()).transform);
    }

    public void SubmitHit(DamageInfo damageInfo, RaycastHit hit)
    {
        var index = FindCollider(hit.collider.transform.parent);
        if(index != null)
        {
            Debug.Log(damageInfo.Attacker.name + "»÷ÖÐÁË" +
            m_bone_enum[index.Value].ToString()
            + "!");
        }
        m_bone_hit.Hit(hit.normal);
        damageInfo.SetTarget(m_owner);
        DamageMgr.Instance.Submit(damageInfo);
    }

    private int? FindCollider(Object obj)
    {
        for(int i = 0; i < m_binds.Count; i++)
        {
            if (m_binds[i] == obj) return i;
        }
        return null;
    }

    public GameObject GetOwner()
    {
        return m_owner;
    }
}
