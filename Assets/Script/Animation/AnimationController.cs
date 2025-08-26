using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    private Movement m_movement;
    public Animator Animator { get { return m_animator; } }

    private readonly string m_die_state_name = "Die";
    private readonly int m_normal_layer = 0;

    private void Awake()
    {
        m_movement = GetComponent<Movement>();
    }
    public void PlayAnim(string anim_name, int anim_layer, float transition_time, float fixed_time)
    {
        m_animator.CrossFadeInFixedTime(anim_name, transition_time, anim_layer, fixed_time);
    }

    private void Update()
    {
        if(m_movement.Movetype == MoveType.None)
            m_animator.SetBool("IsMoving", false);
        else
            m_animator.SetBool("IsMoving", true);
    }

    public void Dead()
    {
        m_animator.PlayInFixedTime(m_die_state_name, m_normal_layer, 0.0f);
    }
}
