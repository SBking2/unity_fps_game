using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] private RectTransform m_hp_transfrom;

    private void OnEnable()
    {
        GameObject.Find("Player").GetComponent<ChaState>().AddListener(UpdateHP);
    }

    private void UpdateHP(float value)
    {
        m_hp_transfrom.sizeDelta =
            new Vector2(value * 500.0f / 100.0f, m_hp_transfrom.sizeDelta.y);
    }
}
