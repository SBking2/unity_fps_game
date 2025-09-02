using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : Singleton<SkillScript>
{
    public Dictionary<string, SkillEvent> m_skill_event_dic;

    public SkillScript()
    {
         m_skill_event_dic = new Dictionary<string, SkillEvent>()
         {
             { "PlayAnim", PlayAnim },
             { "Shoot",  Shoot}
         };
    }

    public SkillEvent GetSkillFunc(string name)
    {
        if(m_skill_event_dic.ContainsKey(name))
        {
            return m_skill_event_dic[name];
        }
        return null;
    }

    private void PlayAnim(TimelineObj time_line_obj, params AnyValue[] args)
    {
        AnimationController ac = time_line_obj.caster.GetComponent<AnimationController>();
        ac.PlayAnim(args[0].stringValue, args[1].intValue, args[2].floatValue, args[3].floatValue);
    }

    private void Shoot(TimelineObj time_line_obj, params AnyValue[] args)
    {
        GameObjectPool.Instance.GetObj(args[1].stringValue, (obj) =>
        {
            obj.transform.position = time_line_obj.caster.transform.position
                + time_line_obj.caster.transform.rotation * args[0].vector3Value;
            ParticleSystem pc = obj.GetComponent<ParticleSystem>();
            pc.Play();

            Bullet bt = obj.GetComponent<Bullet>();
            bt.Initlize(time_line_obj.caster.transform.forward, time_line_obj.caster);
        });
    }

}
