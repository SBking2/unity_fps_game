using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϊһ��buff�ľ�̬����
/// </summary>
public struct BuffModel
{
    public bool is_forever { get; private set; }     //�Ƿ�������Ч
    public float duration { get; private set; }      //����ʱ��
    public float tick { get; private set; }          //��Ч���
}

public delegate void OnRevice(BuffObj buff);    //���buffʱ
public delegate void OnRemove(BuffObj buff);    //ʧȥbuffʱ
public delegate void OnHit(BuffObj buff, DamageInfo damage);    //���е���ʱ����
public delegate void OnHurt(BuffObj buff, DamageInfo damage);   //������ʱ����
public delegate void OnTick(BuffObj buff);      //�������
public delegate void OnDie(BuffObj buff, DamageInfo damage);    //����ʱ�򴥷�