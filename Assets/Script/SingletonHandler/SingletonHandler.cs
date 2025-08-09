using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �õ���ģʽ����ʹ��Mono��һЩ����,������ϣ����̲�����Mono
/// </summary>
public class SingletonHandler : SingletonAutoMono<SingletonHandler>
{

    private void Awake()
    {
        InputManager.Instance.Init();
        PlayerController.Instance.Init();
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        PlayerController.Instance.Update(delta);
    }

    private void OnEnable()
    {
        PlayerController.Instance.OnEnable();
    }

    private void OnDisable()
    {
        PlayerController.Instance.OnDisable();
    }
}
