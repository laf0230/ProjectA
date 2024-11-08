using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletData
{
    public ProjectileType Type { get; set; }
    public float Damage{get;set;}
    public float Speed{get;set;}
    public float Reach{get;set;}
    public Transform Target{get;set;}
    public Transform User{get;set;}
}
