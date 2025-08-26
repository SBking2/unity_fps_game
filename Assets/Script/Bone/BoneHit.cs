using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneHit : MonoBehaviour
{
    private Animator m_animator;
    private Transform m_spine_transform;
    private Quaternion m_offset;

    private void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_spine_transform = m_animator.GetBoneTransform(HumanBodyBones.Spine);
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        ResetBone(delta);
    }

    private void LateUpdate()
    {
        m_spine_transform.rotation *= m_offset;
    }

    public void Hit(Vector3 direct)
    {
        m_offset = Quaternion.LookRotation(direct);
    }

    private void ResetBone(float delta)
    {
        m_offset = Quaternion.Lerp(m_offset, Quaternion.identity, 10.0f * delta);
    }
}
