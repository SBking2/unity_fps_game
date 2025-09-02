using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class HitDetector : MonoBehaviour
{
    protected DamageInfo m_damage_info;
    public HitConifg hit_config;
    public float timer;

    protected Action<Vector3, Vector3> m_hit_event;

    /// <summary>
    /// 产生Hit之后，要更新状态
    /// </summary>
    public virtual void Init()
    {
        timer = 0.0f;
        gameObject.transform.localPosition = hit_config.position;
        gameObject.transform.localRotation = Quaternion.LookRotation(hit_config.direction);

        transform.SetParent(hit_config.father_trans, false);    //设置跟随哪个物体

        m_hit_event = null;
    }

    public void AddListener(Action<Vector3, Vector3> action)
    {
        m_hit_event += action;
    }

    public virtual void UpdateHit(HitConifg config)
    {
        hit_config = config;
        gameObject.layer = config.layer.value == 0 ? 0 : (int)Mathf.Log(config.layer.value, 2);
    }

    public void SetDamageInfo(DamageInfo damage_info)
    {
        m_damage_info = damage_info;
    }

    public abstract void Detecte();
}
