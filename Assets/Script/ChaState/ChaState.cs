using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class ChaState : MonoBehaviour
{
    public float HP { get; private set; }
    public bool IsDie { get; private set; }

    public UnityAction<float> hp_action;

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
}
