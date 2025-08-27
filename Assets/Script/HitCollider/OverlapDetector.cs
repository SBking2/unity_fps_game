using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class OverlapDetector : HitDetector
{
    public override void Init()
    {
        base.Init();
    }
    public override void UpdateHit(HitConifg config)
    {
        base.UpdateHit(config);
    }
    public override void Detecte()
    {
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (hit_config.hit_type != HitType.Overlap) return;

        Gizmos.color = Color.green;

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
