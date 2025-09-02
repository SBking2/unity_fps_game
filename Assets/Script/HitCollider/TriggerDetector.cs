using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager;
using UnityEngine;

public class TriggerDetector : HitDetector
{
    private Collider m_collider;

    public override void Init()
    {
        base.Init();
    }
    public override void UpdateHit(HitConifg config)
    {
        base.UpdateHit(config);
        if(m_collider == null)
        {
            switch(hit_config.shape_type)
            {
                case ShapeType.Sphere:
                    m_collider = transform.AddComponent<SphereCollider>();
                    break;
                case ShapeType.Box:
                    m_collider = transform.AddComponent<BoxCollider>();
                    break;
                case ShapeType.Capsule:
                    m_collider = transform.AddComponent<CapsuleCollider>();
                    break;
            }

            m_collider.isTrigger = true;
        }

        switch (hit_config.shape_type)
        {
            case ShapeType.Sphere:
                SphereCollider sphere = m_collider as SphereCollider;
                sphere.radius = hit_config.raidus;
                break;
            case ShapeType.Box:
                BoxCollider box = m_collider as BoxCollider;
                box.size = hit_config.size;
                break;
            case ShapeType.Capsule:
                CapsuleCollider capsule = m_collider as CapsuleCollider;
                capsule.height = hit_config.height;
                capsule.radius = hit_config.raidus;
                break;
        }
    }
    public override void Detecte()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        int layer_mask = 1 << other.gameObject.layer;
        if((layer_mask & hit_config.target_layer) != 0)
        {
            Vector3 myPos = transform.position;
            Vector3 contactPoint = other.ClosestPoint(myPos);
            Vector3 direction = (myPos - contactPoint).normalized;

            if (m_hit_event != null) m_hit_event(contactPoint, direction);

            m_damage_info.SetTarget(GameObject.Find("Player"));
            DamageMgr.Instance.Submit(m_damage_info);

            HitMgr.Instance.PushHit(this);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (hit_config.hit_type != HitType.Trigger) return;

        Gizmos.color = Color.blue;

        // 设置Gizmos的矩阵，让后续绘制跟随物体旋转
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        switch (hit_config.shape_type)
        {
            case ShapeType.Sphere:
                Gizmos.DrawWireSphere(Vector3.zero, hit_config.raidus);
                break;

            case ShapeType.Box:
                Gizmos.DrawWireCube(Vector3.zero, hit_config.size);
                break;

            case ShapeType.Capsule:
                Vector3 top = Vector3.up * (hit_config.height / 2f);
                Vector3 bottom = -Vector3.up * (hit_config.height / 2f);

                Gizmos.DrawWireSphere(top, hit_config.raidus);
                Gizmos.DrawWireSphere(bottom, hit_config.raidus);

                Gizmos.DrawLine(top + Vector3.forward * hit_config.raidus, bottom + Vector3.forward * hit_config.raidus);
                Gizmos.DrawLine(top - Vector3.forward * hit_config.raidus, bottom - Vector3.forward * hit_config.raidus);
                Gizmos.DrawLine(top + Vector3.right * hit_config.raidus, bottom + Vector3.right * hit_config.raidus);
                Gizmos.DrawLine(top - Vector3.right * hit_config.raidus, bottom - Vector3.right * hit_config.raidus);
                break;
        }

        // 恢复矩阵，避免影响其他Gizmos
        Gizmos.matrix = Matrix4x4.identity;
    }
#endif
}
