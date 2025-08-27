using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class RaycastDetector : HitDetector
{
    private Ray m_ray;

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
                //特效
                GameObjectPool.Instance.GetObj("hit_effect", (obj) =>
                {
                    obj.transform.position = hit_info.point;
                    obj.transform.rotation = Quaternion.LookRotation(hit_info.normal);
                    ParticleSystem effect = obj.GetComponentInChildren<ParticleSystem>();
                    effect.Play();
                });

                CombatColliderHandler combat_collider = hit_info.collider.GetComponentInParent<CombatColliderHandler>();
                if (combat_collider != null)
                {
                    combat_collider.SubmitHit(m_damage_info, hit_info);
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
        Vector3 start = hit_config.position;
        // 射线终点
        Vector3 end = start + hit_config.direction.normalized * hit_config.ray_length;

        Gizmos.DrawLine(start, end);
    }
#endif
}
