using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public interface IMoveable
{
    Animator Animator { get; set; }
    SpriteRenderer Renderer { get; set; }
    Rigidbody Rigidbody { get; set; }
    NavMeshAgent Agent { get; set; }
    bool IsFacingRight { get; set; }
    float StunTime { get; set; }
    void SetMoveAble(bool isActive);
    void MoveTo(Vector3 velopcity);
    void CheckForLeftOrRightFacing(Vector3 velopcity);
}
