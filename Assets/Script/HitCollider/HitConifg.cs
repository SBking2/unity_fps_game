using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    Ray,
    Overlap,
    Trigger
}

public enum ShapeType
{
    Sphere,
    Box,
    Capsule
}

public struct HitConifg
{
    public HitType hit_type;
    public ShapeType shape_type;

    public Transform father_trans;      //Ïë¸úËæµÄtransform

    public LayerMask layer;
    public LayerMask target_layer;

    public float life_time;
    public float tick_time;
    public bool is_immediately;
    public bool is_only_once;

    //Ray
    public Vector3 direction;
    public float ray_length;
    public bool is_cast_all;

    public Vector3 position;

    public float raidus;        //Sphere

    public Vector3 size;    //Box

    public float height;     //Capsule
}
