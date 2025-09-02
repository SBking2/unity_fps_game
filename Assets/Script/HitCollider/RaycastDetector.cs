using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class RaycastDetector : HitDetector
{
    private Ray m_ray;
    private Vector3 point;

    public override void Init()
    {
        base.Init();
    }
    public override void UpdateHit(HitConifg config)
    {
        base.UpdateHit(config);
        m_ray = new Ray(config.position, config.direction);
    }

    public override void Detecte()
    {
        if(!hit_config.is_cast_all)
        {
            RaycastHit hit_info;
            if (Physics.Raycast(m_ray, out hit_info, hit_config.ray_length, hit_config.target_layer))
            {
                if (m_hit_event != null)
                {
                    m_hit_event(hit_info.point, hit_info.normal);
                    print("Debug: ray_hit_event");
                }

                point = hit_info.point;

                CombatColliderHandler combat_collider = hit_info.collider.GetComponentInParent<CombatColliderHandler>();
                if (combat_collider != null)
                {
                    combat_collider.SubmitHit(m_damage_info, hit_info.collider.gameObject, hit_info.normal);
                }
            }
        }else
        {
            var hit_infos = Physics.RaycastAll(m_ray, hit_config.ray_length, hit_config.target_layer);
            if(hit_infos.Length > 0)
            {

            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (hit_config.hit_type != HitType.Ray) return;

        Gizmos.color = Color.red;

        // 射线起点
        Vector3 start = transform.position;
        // 射线终点
        Vector3 end = start + hit_config.direction.normalized * hit_config.ray_length;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireCube(point, Vector3.one * 0.1f);
    }
#endif
}
