using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj
{
    public BuffModel model;
    public GameObject Caster { get; private set; }      //buff���ͷ���
    public GameObject Owner { get; private set; }       //buff�ĳ�����

    public float timer;     //��ʱ��

}
