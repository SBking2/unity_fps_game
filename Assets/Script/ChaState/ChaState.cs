using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaState : MonoBehaviour
{
    public float HP { get; private set; }
    public UnityAction<float> hp_action;

    private void Start()
    {
        HP = 100;
        StartCoroutine(Decrease());
    }

    private void Update()
    {
        if(hp_action != null)
        {
            hp_action(HP);
        }

    }

    private IEnumerator Decrease()
    {
        while(HP > 0)
        {
            yield return new WaitForSeconds(0.5f);
            HP -= 3.0f;
        }
    }

    private void OnDisable()
    {
        hp_action = null;
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
