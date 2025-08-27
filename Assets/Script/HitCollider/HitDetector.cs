using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitDetector : MonoBehaviour
{
    protected DamageInfo m_damage_info;
    public HitConifg hit_config;

    public float timer;

    /// <summary>
    /// 产生Hit之后，要更新状态
    /// </summary>
    public virtual void Init()
    {
        timer = 0.0f;
    }

    public virtual void UpdateHit(HitConifg config)
    {
        hit_config = config;
        gameObject.layer = config.layer;
    }

    public void SetDamageInfo(DamageInfo damage_info)
    {
        m_damage_info = damage_info;
    }

    public abstract void Detecte();
}
