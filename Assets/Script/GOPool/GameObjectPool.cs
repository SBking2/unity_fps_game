using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : Singleton<GameObjectPool>
{
    private Dictionary<string, Stack<GameObject>> m_obj_dic = new Dictionary<string, Stack<GameObject>>();
    private GameObject m_root;
    public GameObjectPool()
    {
        m_root = new GameObject("Pool");
    }

    public GameObject GetObj(string name)
    {
        if(m_obj_dic.ContainsKey(name))
        {
            if (m_obj_dic[name].Count > 0)
            {
                GameObject obj = m_obj_dic[name].Pop();
                obj.transform.SetParent(null);
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject obj_res = LoadGameObject(name);
        GameObject obj_instance = GameObject.Instantiate(obj_res);
        obj_instance.name = name;
        return obj_instance;
    }

    public void PushObj(GameObject obj)
    {
        if(!m_obj_dic.ContainsKey(obj.name))
        {
            m_obj_dic.Add(obj.name, new Stack<GameObject>());
        }

        obj.transform.SetParent(m_root.transform, false);
        obj.SetActive(false);
        m_obj_dic[obj.name].Push(obj);
    }

    private GameObject LoadGameObject(string name)
    {
        GameObject obj_res = Resources.Load<GameObject>(name);
        return obj_res;
    }
}
