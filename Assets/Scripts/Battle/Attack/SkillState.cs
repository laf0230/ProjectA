
public interface SkillState 
{
    // 필요한 요소 구상
    public virtual void AttackTiming() { } // 데미지가 들어가는 시점
    public virtual void StartAttack() { } // 시작하는 시간
    public virtual void EndAttack() { } //  끝나는 시간
    public virtual void StartRestriction() { } // 방해가 가능한 시점
    public virtual void EndRestriction() { } // 방해가 불가능한 시점
}