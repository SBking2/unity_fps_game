using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �˽ű���������һЩģ��
/// </summary>
public class PreLoad : SingletonMono<PreLoad>
{
    private void Start()
    {
        SingletonHandler.GetInstance();
    }
}
