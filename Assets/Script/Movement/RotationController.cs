using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private float m_rotation_speed;
    private Quaternion m_target_rotation;

    private Movement m_movement;

    private void Awake()
    {
        m_movement = GetComponent<Movement>();
        m_target_rotation = transform.rotation;
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        transform.rotation = Quaternion.Lerp(transform.rotation, m_target_rotation
            , delta * m_rotation_speed);

        Vector3 direct = m_movement.GetMoveDirect();
        SetLookRotation(direct);
    }

    public void SetLookRotation(Vector3 direct)
    {
        direct.y = 0.0f;
        if(direct != Vector3.zero)
            m_target_rotation = Quaternion.LookRotation(direct);
    }
}
