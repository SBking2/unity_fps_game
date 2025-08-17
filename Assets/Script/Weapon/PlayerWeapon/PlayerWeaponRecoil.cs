using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponRecoil
{
    #region model
    private Vector3 m_rota_recoil_amount = new Vector3(3.0f, 5.0f, 0.0f);
    private Vector3 m_pos_recoil_amount = new Vector3(0.01f, 0.01f, 0.02f);
    private float m_recoil_decerleration = 5.0f;
    #endregion

    private Transform m_weapon_recoil_transform;

    public PlayerWeaponRecoil(Transform weapon_recoil_transform)
    {
        m_weapon_recoil_transform = weapon_recoil_transform;
    }

    public void Update(float delta, Vector2 mul)
    {
        m_weapon_recoil_transform.localPosition = Vector3.Lerp(
            m_weapon_recoil_transform.localPosition, Vector3.zero, m_recoil_decerleration * delta);

        //根据相机的偏移，做武器的一些偏移
        Vector3 weapon_rota = new Vector3(
            m_rota_recoil_amount.x * mul.x,
            m_rota_recoil_amount.y * mul.y,
            0.0f
            );
        m_weapon_recoil_transform.localRotation = Quaternion.Lerp(
            m_weapon_recoil_transform.localRotation,
            Quaternion.Euler(weapon_rota),
            20.0f * delta
            );
    }

    public void Recoil(float recoil_extent)
    {
        m_weapon_recoil_transform.localPosition += new Vector3(
            Random.Range(m_pos_recoil_amount.x, m_pos_recoil_amount.x * 2),
            Random.Range(m_pos_recoil_amount.y, m_pos_recoil_amount.y * 2),
            -Random.Range(m_pos_recoil_amount.z, m_pos_recoil_amount.z * 2)
            ) * recoil_extent;
    }
}
