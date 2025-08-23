using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMgr : Singleton<DamageMgr>
{
    public void Submit(DamageInfo info)
    {
        Debug.Log(info.Attacker.name + "对" + info.Target.name + "产生了一次Damege!");

        if(info.Attacker != null)
        {
            
        }
    }
}
