using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public interface IHitContainer
{
    public HitDetector Pop(HitConifg config);       //要根据config更新collider
    public void Push(HitDetector hit_derector);
}

public class RayContainer : IHitContainer
{
    public Stack<HitDetector> m_hits = new Stack<HitDetector>();
    public HitDetector Pop(HitConifg config)
    {
        if(m_hits.Count > 0)
        {
            var hit = m_hits.Pop();
            hit.UpdateHit(config);
            hit.gameObject.SetActive(true);
            return hit;
        }
        return null;
    }
    public void Push(HitDetector hit_derector)
    {
        hit_derector.gameObject.SetActive(false);
        m_hits.Push(hit_derector);
    }
}
public class TriggerContainer : IHitContainer
{
    private Stack<HitDetector> m_sphere_hits = new Stack<HitDetector>();
    private Stack<HitDetector> m_box_hits = new Stack<HitDetector>();
    private Stack<HitDetector> m_capsule_hits = new Stack<HitDetector>();

    public HitDetector Pop(HitConifg config)
    {
        HitDetector hit = null;
        switch(config.shape_type)
        {
            case ShapeType.Sphere:
                if(m_sphere_hits.Count > 0)
                    hit = m_sphere_hits.Pop();
                break;
            case ShapeType.Box:
                if (m_box_hits.Count > 0)
                    hit = m_box_hits.Pop();
                break;
            case ShapeType.Capsule:
                if (m_capsule_hits.Count > 0)
                    hit = m_capsule_hits.Pop();
                break;
        }

        if(hit != null)
        {
            hit.UpdateHit(config);
            hit.gameObject.SetActive(true);
        }

        return hit;
    }
    public void Push(HitDetector hit_derector)
    {
        hit_derector.gameObject.SetActive(false);
        switch (hit_derector.hit_config.shape_type)
        {
            case ShapeType.Sphere:
                m_sphere_hits.Push(hit_derector);
                break;
            case ShapeType.Box:
                m_box_hits.Push(hit_derector);
                break;
            case ShapeType.Capsule:
                m_capsule_hits.Push(hit_derector);
                break;
        }
    }
}
public class OverlapContainer : IHitContainer
{
    private Stack<HitDetector> m_sphere_hits = new Stack<HitDetector>();
    private Stack<HitDetector> m_box_hits = new Stack<HitDetector>();
    private Stack<HitDetector> m_capsule_hits = new Stack<HitDetector>();

    public HitDetector Pop(HitConifg config)
    {
        HitDetector hit = null;
        switch (config.shape_type)
        {
            case ShapeType.Sphere:
                if (m_sphere_hits.Count > 0)
                    hit = m_sphere_hits.Pop();
                break;
            case ShapeType.Box:
                if (m_box_hits.Count > 0)
                    hit = m_box_hits.Pop();
                break;
            case ShapeType.Capsule:
                if (m_capsule_hits.Count > 0)
                    hit = m_capsule_hits.Pop();
                break;
        }

        if (hit != null)
        {
            hit.UpdateHit(config);
            hit.gameObject.SetActive(true);
        }

        return hit;
    }
    public void Push(HitDetector hit_derector)
    {
        hit_derector.gameObject.SetActive(false);
        switch (hit_derector.hit_config.shape_type)
        {
            case ShapeType.Sphere:
                m_sphere_hits.Push(hit_derector);
                break;
            case ShapeType.Box:
                m_box_hits.Push(hit_derector);
                break;
            case ShapeType.Capsule:
                m_capsule_hits.Push(hit_derector);
                break;
        }
    }
}

public class HitMgr : Singleton<HitMgr>
{
    //ray、overlap、trigger分开存储池子
    public Dictionary<HitType, IHitContainer> m_hit_dic;
    public List<HitDetector> m_running_hit = new List<HitDetector>();

    public GameObject m_father;

    public void Init()
    {
        m_hit_dic = new Dictionary<HitType, IHitContainer>()
        {
            { HitType.Ray, new RayContainer() },
            { HitType.Overlap, new OverlapContainer() },
            { HitType.Trigger, new TriggerContainer() }
        };

        if(m_father == null)
        {
            m_father = new GameObject("HitPool");
        }
    }

    public void InitiateHit(DamageInfo damage_info, HitConifg config)
    {
        var hit = m_hit_dic[config.hit_type].Pop(config);       //尝试从容器里取
        if(hit == null)
        {
            GameObject obj = new GameObject("Hit");
            switch(config.hit_type)
            {
                case HitType.Ray:
                    hit = obj.AddComponent<RaycastDetector>();
                    break;
                case HitType.Overlap:
                    hit = obj.AddComponent<OverlapDetector>();
                    break;
                case HitType.Trigger:
                    hit = obj.AddComponent<TriggerDetector>();
                    break;
            }
            hit.UpdateHit(config);
        }
        hit.SetDamageInfo(damage_info);
        hit.Init();
        hit.transform.SetParent(null, false);

        hit.gameObject.transform.position = config.position;
        hit.gameObject.transform.rotation = Quaternion.LookRotation(config.direction);

        if (config.is_immediately)
            hit.Detecte();

        m_running_hit.Add(hit);
    }

    //TODO:处理tick、immediately等属性
    public void Update(float delta)
    {
        int insert_index = -1;
        for(int i = 0; i < m_running_hit.Count; i++)
        {
            m_running_hit[i].timer += delta;

            if(m_running_hit[i].timer >= m_running_hit[i].hit_config.life_time)
            {
                m_running_hit[i].transform.SetParent(m_father.transform, false);
                m_hit_dic[m_running_hit[i].hit_config.hit_type].Push(m_running_hit[i]);     //塞回到池子里面
                if (insert_index == -1) insert_index = i;
            }else
            {
                if (insert_index != -1)
                {
                    m_running_hit[insert_index] = m_running_hit[i];
                    insert_index++;
                }
            }
        }
        if(insert_index != -1)
        {
            int count = m_running_hit.Count;
            for (int i = 0; i < count - insert_index; i++)
            {
                m_running_hit.RemoveAt(m_running_hit.Count - 1);
            }
        }
    }
}
