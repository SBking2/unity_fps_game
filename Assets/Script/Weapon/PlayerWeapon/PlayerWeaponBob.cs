using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponBob
{
    #region model
    private float m_deceleration = 10.0f;
    private float m_frequency = 5.0f;
    private float m_amplitude = 0.03f;
    #endregion

    private bool m_is_weaponbob_started = false;

    private Transform m_weapon_transform;

    public PlayerWeaponBob(Transform weapon_transform)
    {
        m_weapon_transform = weapon_transform;
    }

    public void Update(float delta, float amp_multipler, float fre_multipler, bool is_ground)
    {
        ResetBob(delta);
        if (is_ground)
            BobStep(delta, amp_multipler, fre_multipler);
    }

    private void ResetBob(float delta)
    {
        m_weapon_transform.localPosition = Vector3.Lerp(m_weapon_transform.localPosition, Vector3.zero, m_deceleration * delta);
    }

    private void BobStep(float delta, float amp_mul, float fre_mul)
    {
        float _frequency = fre_mul * m_frequency;
        float _amplitude = amp_mul * m_amplitude;

        Vector3 pos = Vector3.zero;
        pos.x += Mathf.Sin(Time.time * _frequency) * _amplitude * 2;
        pos.y += Mathf.Abs(Mathf.Cos(Time.time * _frequency)) * _amplitude;

        if ((m_weapon_transform.localPosition - pos).magnitude <= 0.01f)
            m_is_weaponbob_started = true;
        else
            m_is_weaponbob_started = false;

        if (m_is_weaponbob_started)
        {
            m_weapon_transform.localPosition = pos;
        }
        else
        {
            m_weapon_transform.localPosition = Vector3.Lerp(
                m_weapon_transform.localPosition, pos,
                3.0f * delta);
        }
    }
}
