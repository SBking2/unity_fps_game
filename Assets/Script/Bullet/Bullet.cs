using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private HitConfigSO m_hitconfig;

    private GameObject m_owner;
    private DamageInfo m_info;

    private Vector3 m_direct;

    public void Initlize(Vector3 direct, GameObject owner)
    {
        transform.rotation = Quaternion.LookRotation(direct);
        m_direct = direct;
        this.m_owner = owner;

        m_info = new DamageInfo();
        m_info.SetAttacker(m_owner);
        m_info.SetValue(5);

        m_hitconfig.config.father_trans = this.transform;

        HitMgr.Instance.InitiateHit(m_info, m_hitconfig.config).AddListener((point, normal) =>
        {
            GameObjectPool.Instance.PushObj(this.gameObject);
            GameObjectPool.Instance.GetObj("hit_effect", (obj) =>
            {
                obj.transform.position = point;
                obj.transform.rotation = Quaternion.LookRotation(normal);
                ParticleSystem effect = obj.GetComponentInChildren<ParticleSystem>();
                effect.Play();
            });
        });
    }

    private void Update()
    {
        transform.position += m_direct * 20.0f * Time.deltaTime;
    }
}
