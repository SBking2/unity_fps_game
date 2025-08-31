using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 命令中枢,其他组件想给Cha下命令，要通过ChaState处理
/// </summary>
public class ChaState : MonoBehaviour
{
    public float HP { get; private set; }
    public bool IsDie { get; private set; }

    public UnityAction<float> hp_action;

    private MovementController m_movement;
    private RotationController m_rotation_controller;
    private AnimationController m_animation_controller;

    private void Awake()
    {
        m_movement = GetComponent<MovementController>();
        m_rotation_controller = GetComponent<RotationController>();
        m_animation_controller = GetComponent<AnimationController>();
    }

    private void Start()
    {
        HP = 100;
        IsDie = false;
    }

    private void Update()
    {
        if(hp_action != null)
        {
            hp_action(HP);
        }

        if(IsDie == false && HP < 0)
        {
            Die();
        }

        if(m_movement.GetMoveDirect() != Vector3.zero)
        {
            if(m_rotation_controller != null)
                m_rotation_controller.SetLookRotation(m_movement.GetMoveDirect());
        }

    }

    private void OnDisable()
    {
        hp_action = null;
    }

    private void Die()
    {
        IsDie = true;
        AnimationController ac = GetComponent<AnimationController>();
        if (ac != null)
            ac.Dead();
    }

    public void Hurt(float damage)
    {
        HP -= damage;
    }

    public void AddListener(UnityAction<float> action)
    {
        hp_action += action;
    }
    public void RemoveListner(UnityAction<float> action)
    {
        hp_action -= action;
    }

    public void PlayAnim(string anim_name, int anim_layer, float transition_time, float fixed_time)
    {
        m_animation_controller.PlayAnim(anim_name, anim_layer, transition_time, fixed_time);
    }

    public void MoveDirect(Vector3 direct)
    {
        //TODO:要加处理，比如放技能过程不能移动。。。啥的
        m_movement.SetMoveDirect(direct);
    }
}
