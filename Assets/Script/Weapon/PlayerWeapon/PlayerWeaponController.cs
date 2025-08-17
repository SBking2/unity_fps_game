using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponController
{
    private PlayerWeaponAttack m_weapon_attack;
    private PlayerWeaponBob m_weapon_bob;
    private PlayerWeaponSway m_weapon_sway;

    public PlayerWeaponController(Transform shoot_dir_transform, Transform weapon_bob_transform
        , Transform weapon_sway_transform, LayerMask attack_layer)
    {
        m_weapon_attack = new PlayerWeaponAttack(shoot_dir_transform, attack_layer);
        m_weapon_bob = new PlayerWeaponBob(weapon_bob_transform);
        m_weapon_sway = new PlayerWeaponSway(weapon_sway_transform);
    }
    public void Update(float delta, float amp_mul, float fre_mul, bool is_ground, Vector2 mouse_delta)
    {
        m_weapon_bob.Update(delta, amp_mul, fre_mul, is_ground);
        m_weapon_attack.Update(delta);
        m_weapon_sway.Update(delta, mouse_delta);
    }
    public void SetAttackSignal(bool value)
    {
        m_weapon_attack.SetAttackSignal(value);
    }
    public void AddFireListener(UnityAction<float> callback)
    {
        m_weapon_attack.onFireEvent += callback;
    }
    public void RemoveFireListener(UnityAction<float> callback)
    {
        m_weapon_attack.onFireEvent -= callback;
    }
}
