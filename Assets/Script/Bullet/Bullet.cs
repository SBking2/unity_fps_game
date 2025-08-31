using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        
    }

    private Vector3 m_direct;

    public void Initlize(Vector3 direct)
    {
        transform.rotation = Quaternion.LookRotation(direct);
        m_direct = direct;
    }

    private void Update()
    {
        transform.position += m_direct * 20.0f * Time.deltaTime;
    }
}
