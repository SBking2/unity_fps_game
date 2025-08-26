using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public GameObject Attacker {  get; private set; }
    public GameObject Target {  get; private set; }

    public float Value { get; private set; }

    public void SetAttacker(GameObject attacker) {  Attacker = attacker; }
    public void SetTarget(GameObject target) { Target = target; }
    public void SetValue(float value) {  Value = value; }
}
