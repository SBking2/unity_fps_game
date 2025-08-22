
using UnityEngine;

public class PlayerRecoil
{
    #region model
    private float m_extent_deceleration = 5.0f;
    private float m_extent_acceleration = 70.0f;
    #endregion

    private float m_recoil_extent = 0.0f;   //Recoil的程度，当持续开火时，extent会越来越大，偏移的随机程度会更大

    private PlayerWeaponRecoil m_weapon_recoil;
    private PlayerCameraRecoil m_camera_recoil;

    public PlayerRecoil(Transform weapon_recoil_transform, Transform camera_recoil_transform
        , Transform shoot_dir_transform)
    {
        m_weapon_recoil = new PlayerWeaponRecoil(weapon_recoil_transform);
        m_camera_recoil = new PlayerCameraRecoil(camera_recoil_transform, shoot_dir_transform);
    }

    public void Update(float delta)
    {
        m_recoil_extent = Mathf.Lerp(m_recoil_extent, 0.0f, m_extent_deceleration * delta);
        m_weapon_recoil.Update(delta, m_camera_recoil.GetRotaMul());
        m_camera_recoil.Update(delta);
    }

    public void Recoil(float delta)
    {
        m_recoil_extent += m_extent_acceleration * delta;
        m_recoil_extent = Mathf.Clamp01(m_recoil_extent);
        m_camera_recoil.Recoil(m_recoil_extent);
        m_weapon_recoil.Recoil(m_recoil_extent);
    }
}