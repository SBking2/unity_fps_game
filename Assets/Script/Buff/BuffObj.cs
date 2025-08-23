using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj
{
    public BuffModel model;
    public GameObject Caster { get; private set; }      //buff的释放者
    public GameObject Owner { get; private set; }       //buff的持有者

    public float timer;     //计时器

}
