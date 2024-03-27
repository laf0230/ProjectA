using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetingable
{
    GameObject Target { get; set; }
    int ThreatLevel { get; set; }
    void SetTarget(GameObject target);
}
