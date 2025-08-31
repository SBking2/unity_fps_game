using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO / Skill Time line", fileName = "SkillTimeline")]
public class Timeline : ScriptableObject
{
    public List<TimelineNode> nodes;
    public float duration;
}

public delegate void SkillEvent(TimelineObj time_line_obj, params AnyValue[] args);
