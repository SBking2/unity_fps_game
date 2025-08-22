using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRecoil
{
    #region model
    private float m_recoil_decerleration = 2.0f;
    private float m_recoil_follow_speed = 10.0f;
    private Vector3 m_camera_recoil_amount = new Vector3(5.0f, 3.0f, 0.0f);
    private float m_shoot_dir_multipiler = 2.0f;    //基于相机偏移的射击方向偏移倍数
    #endregion

    private Transform m_camera_recoil_transform;    //后座力导致的镜头偏移
    private Transform m_shoot_dir_transform;     //后座力导致的射击偏移（即射击的位置不在准星上）

    private Vector3 m_target_camera_euler;

    public PlayerCameraRecoil(Transform camera_recoil_transform, Transform shoot_dir_transform)
    {
        m_camera_recoil_transform = camera_recoil_transform;
        m_shoot_dir_transform = shoot_dir_transform;
    }

    public void Update(float delta)
    {
        //相机偏移趋近于0
        m_target_camera_euler = Vector3.Lerp(
            m_target_camera_euler, Vector3.zero, m_recoil_decerleration * delta);

        m_camera_recoil_transform.localRotation = Quaternion.Lerp(
            m_camera_recoil_transform.localRotation, Quaternion.Euler(m_target_camera_euler), m_recoil_follow_speed * delta);

        //射击方向偏移
        Vector3 shoot_dir_euler = m_target_camera_euler * m_shoot_dir_multipiler;
        m_shoot_dir_transform.localRotation = Quaternion.Lerp(
            m_shoot_dir_transform.localRotation, Quaternion.Euler(shoot_dir_euler), m_recoil_follow_speed * delta);
    }

    public void Recoil(float recoil_extent)
    {
        m_target_camera_euler += new Vector3(
            -m_camera_recoil_amount.x
            , Random.Range(-m_camera_recoil_amount.y, m_camera_recoil_amount.y)
            , 0.0f
            ) * recoil_extent;
    }

    public Vector2 GetRotaMul()
    {
        Vector2 mul = new Vector2(
            m_target_camera_euler.x / m_camera_recoil_amount.x
            , m_target_camera_euler.y / m_camera_recoil_amount.y
            );
        return mul;
    }
}
