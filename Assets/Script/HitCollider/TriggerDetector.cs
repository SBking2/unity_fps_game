using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
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
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (hit_config.hit_type != HitType.Trigger) return;

        Gizmos.color = Color.blue;

        switch (hit_config.shape_type)
        {
            case ShapeType.Sphere:
                Gizmos.DrawWireSphere(hit_config.position, hit_config.raidus);
                break;
            case ShapeType.Box:
                Gizmos.DrawWireCube(hit_config.position, hit_config.size);
                break;

            case ShapeType.Capsule:
                Vector3 top = hit_config.position + Vector3.up * (hit_config.height / 2f);
                Vector3 bottom = hit_config.position - Vector3.up * (hit_config.height / 2f);

                Gizmos.DrawWireSphere(top, hit_config.raidus);
                Gizmos.DrawWireSphere(bottom, hit_config.raidus);
                Gizmos.DrawLine(top + Vector3.forward * hit_config.raidus, bottom + Vector3.forward * hit_config.raidus);
                Gizmos.DrawLine(top - Vector3.forward * hit_config.raidus, bottom - Vector3.forward * hit_config.raidus);
                Gizmos.DrawLine(top + Vector3.right * hit_config.raidus, bottom + Vector3.right * hit_config.raidus);
                Gizmos.DrawLine(top - Vector3.right * hit_config.raidus, bottom - Vector3.right * hit_config.raidus);
                break;
        }
    }
#endif
}
