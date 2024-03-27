using UnityEngine;

public interface IMoveable
{
    SpriteRenderer Renderer { get; set; }
    Rigidbody Rigidbody { get; set; }
    bool IsFacingRight { get; set; }
    void MoveEnemy(Vector3 velopcity);
    void CheckForLeftOrRightFacing(Vector3 velopcity);
}
