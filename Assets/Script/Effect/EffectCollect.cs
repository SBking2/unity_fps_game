using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCollect : MonoBehaviour
{
    [SerializeField] private GameObject m_collect_obj;
    private void OnParticleSystemStopped()
    {
        GameObjectPool.Instance.PushObj(m_collect_obj);
    }
}
