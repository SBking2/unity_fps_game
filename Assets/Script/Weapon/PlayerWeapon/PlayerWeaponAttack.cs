using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class PlayerWeaponAttack
{
    #region model
    private float m_attack_gap = 0.2f;
    #endregion

    private Transform m_shoot_dir_transform;
    private LayerMask m_attack_layer;
    
    private float m_attack_timer;
    private bool m_is_attack_flag = false;
    private bool m_is_can_attack = false;

    public event UnityAction<float> onFireEvent;

    public PlayerWeaponAttack(Transform shoot_dir_transform, LayerMask attack_layer)
    {
        m_attack_timer = m_attack_gap;
        m_shoot_dir_transform = shoot_dir_transform;
        m_attack_layer = attack_layer;
    }

    public void Update(float delta)
    {
        m_attack_timer += delta;
        if (m_attack_timer > m_attack_gap)
            m_is_can_attack = true;
        else
            m_is_can_attack = false;

        if (m_is_can_attack && m_is_attack_flag)
            Fire(delta);
    }

    private void Fire(float delta)
    {
        m_attack_timer = 0.0f;

        Ray ray = new Ray(m_shoot_dir_transform.position, m_shoot_dir_transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100.0f, m_attack_layer))
        {
            GameObject hit_effect =  GameObjectPool.Instance.GetObj("hit_effect");
            hit_effect.transform.position = hit.point;
            hit_effect.transform.rotation = Quaternion.LookRotation(hit.normal);
            ParticleSystem effect = hit_effect.GetComponentInChildren<ParticleSystem>();
            effect.Play();
            Debug.Log(hit.collider.gameObject.name);
        }

        if (onFireEvent != null) onFireEvent(delta);
    }

    public void SetAttackSignal(bool value)
    {
        m_is_attack_flag = value;
    }
}
