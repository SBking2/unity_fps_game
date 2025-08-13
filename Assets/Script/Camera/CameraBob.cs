using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob
{
    private Transform m_camera_bob_transfrom;
    private Transform m_camera_handler;
    private float m_deceleration;
    private float m_frequency = 10.0f;
    private float m_amplitude = 0.03f;

    private bool m_is_camerabob_started = false;

    public CameraBob(Transform camera_bob, Transform camera_handler)
    {
        m_camera_bob_transfrom = camera_bob;
        m_camera_handler = camera_handler;
    }

    public void Update(float delta, float amp_multipler, float fre_multipler)
    {
        ResetPos();
        BobStep(delta, amp_multipler, fre_multipler);
        FocusTarget();
    }

    private void ResetPos()
    {
        m_camera_bob_transfrom.localPosition = Vector3.Lerp(m_camera_bob_transfrom.localPosition, Vector3.zero, m_deceleration * Time.deltaTime);
        m_camera_bob_transfrom.localRotation = Quaternion.Lerp(m_camera_bob_transfrom.localRotation, Quaternion.identity, m_deceleration * Time.deltaTime);
    }

    private void FocusTarget()
    {
        Vector3 pos = m_camera_handler.transform.position + m_camera_handler.transform.forward * 15f;
        m_camera_bob_transfrom.LookAt(pos);
    }

    private void BobStep(float delta, float amp_mul, float fre_mul)
    {
        float _frequency = fre_mul * m_frequency;
        float _amplitude = amp_mul * m_amplitude;

        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;

        if ((m_camera_bob_transfrom.localPosition - pos).magnitude <= 0.01f)
            m_is_camerabob_started = true;
        else
            m_is_camerabob_started = false;

        if (m_is_camerabob_started)
        {
            m_camera_bob_transfrom.localPosition = pos;
        }else
        {
            m_camera_bob_transfrom.localPosition = Vector3.Lerp(
                m_camera_bob_transfrom.localPosition, pos,
                10.0f * delta);
        }
    }
}
