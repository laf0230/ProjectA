using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_HunterKiller : SpecialSkill
{
    public string characterTag = "Character";
    public string clockedTag = "Untagged";
    public float CockingDuration = 3f;
    public bool isClock = false;
    public Color ClockedColor = new Color(1, 1, 1, 0.1f);
    public Color BasicColor = new Color(1, 1, 1, 1);

    IEnumerator OverClock()
    {
        float currentSpeed = Character.ChaseSpeed;

        Debug.Log("CLOCKING");
        gameObject.tag = clockedTag;
        isClock = true;
        Character.ChaseSpeed += currentSpeed + currentSpeed * 0.1f;
        yield return new WaitForSeconds(CockingDuration);
        Character.Renderer.color = BasicColor;
        gameObject.tag = characterTag;
        isClock = false;
        Character.ChaseSpeed = currentSpeed;
    }

    public override void SS_AT()
    {
        base.Character.Renderer.color = ClockedColor;
        StartCoroutine(OverClock());
    }
}
