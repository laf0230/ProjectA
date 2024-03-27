using UnityEngine;

public interface ITriggerCheckable
{
    bool IsAggroed { get; set; }
    bool IsWithinstrikingDistance { get; set; }
    bool IsPassThrough { get; set; }
    void SetAggrostatus(bool isAggroed);
    void SetStrikingDistance(bool isWithinstrikingDistance);
}
