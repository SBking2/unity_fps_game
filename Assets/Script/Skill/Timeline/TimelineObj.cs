using UnityEngine;

public class TimelineObj
{
    public TimelineObj(GameObject caster, Timeline model)
    {
        this.caster = caster;
        time_line_model = model;

        //初始化脚本函数
        foreach(var node in time_line_model.nodes)
        {
            node.skill_event = SkillScript.Instance.GetSkillFunc(node.event_name);
        }
    }

    public GameObject caster;   //施法者
    public float timer;
    public Timeline time_line_model;
}