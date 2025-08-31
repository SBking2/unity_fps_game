using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class AnyValue
{
    public enum ValueType { Int, Float, String, Vector3 }  // ✅ 加了 Vector3
    public ValueType type;

    public int intValue;
    public float floatValue;
    public string stringValue;
    public Vector3 vector3Value; // ✅ 新增

    public object GetValue()
    {
        switch (type)
        {
            case ValueType.Int: return intValue;
            case ValueType.Float: return floatValue;
            case ValueType.String: return stringValue;
            case ValueType.Vector3: return vector3Value;
            default: return null;
        }
    }
}


