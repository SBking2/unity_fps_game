using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMgr : Singleton<DamageMgr>
{
    public void Submit(DamageInfo info)
    {
        Debug.Log(info.Attacker.name + "��" + info.Target.name + "������һ��Damege!");

        if(info.Attacker != null)
        {
            
        }
    }
}
