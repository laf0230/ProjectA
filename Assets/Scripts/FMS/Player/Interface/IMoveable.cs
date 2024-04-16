using System.Collections;
using UnityEngine;

public interface IMoveable
{
    Animator Animator { get; set; }
    SpriteRenderer Renderer { get; set; }
    Rigidbody Rigidbody { get; set; }
    bool IsFacingRight { get; set; }
    float StunTime { get; set; }
    void MoveTo(Vector3 velopcity);
    void CheckForLeftOrRightFacing(Vector3 velopcity);
    IEnumerator Stun();
}
