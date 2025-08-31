using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO / Skill Time line Node", fileName = "SkillTimelineNode")]
public class TimelineNode : ScriptableObject
{
    public float time_elapsed;
    public string event_name;
    public AnyValue[] args;
    public SkillEvent skill_event;
}
