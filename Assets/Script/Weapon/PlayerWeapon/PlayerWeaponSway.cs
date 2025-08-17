using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponSway
{

    #region model
    private Vector2 m_sway_amout = new Vector2(3.0f, 1.0f);
    private float m_sway_return_speed = 10.0f;
    private float m_sway_follow_speed = 20.0f;
    #endregion

    #region obj
    private Vector3 m_target_euler;
    #endregion

    private Transform m_weapon_sway_transform;

    public PlayerWeaponSway(Transform sway_transform)
    {
        m_weapon_sway_transform = sway_transform;
    }
    public void Update(float delta, Vector2 mouse_delta)
    {
        ResetSway(delta);

        m_target_euler += new Vector3(
            +(mouse_delta.y * m_sway_amout.x) * delta,
            -(mouse_delta.x * m_sway_amout.y) * delta,
            .0f
            );

        m_weapon_sway_transform.localRotation = Quaternion.Slerp(m_weapon_sway_transform.localRotation
            , Quaternion.Euler(m_target_euler)
            , m_sway_follow_speed * delta);
    }

    private void ResetSway(float delta)
    {
        m_target_euler = 
            Vector3.Lerp(m_target_euler, Vector3.zero, m_sway_return_speed * delta);
    }
}
