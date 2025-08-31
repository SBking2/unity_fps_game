using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public bool IsSkilling {  get; private set; }
    private TimelineObj m_current_skill;

    public void CasteSkill(Timeline time_line)
    {
        m_current_skill = new TimelineObj(this.gameObject, time_line);
        IsSkilling = true;
    }

    private void Update()
    {
        float delta = Time.deltaTime; 
        HandleTimeline(delta);
    }

    private void HandleTimeline(float delta)
    {
        if (m_current_skill == null) return;

        float elapsed = m_current_skill.timer;
        m_current_skill.timer += delta;

        foreach(var node in m_current_skill.time_line_model.nodes)
        {
            if (m_current_skill.timer >= node.time_elapsed
                && elapsed <= node.time_elapsed)
            {
                if(node.skill_event != null)
                    node.skill_event.Invoke(m_current_skill, node.args);      //´¥·¢º¯Êý
            }
        }

        if(m_current_skill.timer >= m_current_skill.time_line_model.duration)
        {
            m_current_skill = null;
            IsSkilling = false;
            return;     //Ïú»ÙtimelineObj
        }
    }
}
