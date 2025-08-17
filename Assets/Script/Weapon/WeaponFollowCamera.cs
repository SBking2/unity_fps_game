using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform m_target;

    private void LateUpdate()
    {
        transform.position = m_target.position;
        transform.rotation = m_target.rotation;
    }
}
