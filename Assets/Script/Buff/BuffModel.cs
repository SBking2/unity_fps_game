using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作为一个buff的静态属性
/// </summary>
public struct BuffModel
{
    public bool is_forever { get; private set; }     //是否永久生效
    public float duration { get; private set; }      //持续时间
    public float tick { get; private set; }          //生效间隔
}

public delegate void OnRevice(BuffObj buff);    //获得buff时
public delegate void OnRemove(BuffObj buff);    //失去buff时
public delegate void OnHit(BuffObj buff, DamageInfo damage);    //击中敌人时触发
public delegate void OnHurt(BuffObj buff, DamageInfo damage);   //被击中时触发
public delegate void OnTick(BuffObj buff);      //间隔触发
public delegate void OnDie(BuffObj buff, DamageInfo damage);    //死的时候触发