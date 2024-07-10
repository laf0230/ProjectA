using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_HunterKiller : SpecialSkill
{
    public float CockingDuration = 10f;
    public bool isClock = false;
    public Color ClockedColor = new Color(255, 255, 255, 50);
    public Color BasicColor = new Color(255, 255, 255, 255);

    IEnumerator OverClock()
    {
        Character.Renderer.color = ClockedColor;
        isClock = true;
        yield return new WaitForSeconds(CockingDuration);
        Character.Renderer.color = BasicColor;
        isClock = false;
        Debug.Log("CLOCKING");
    }

    public override void SS_EA()
    {
        base.SS_EA();

        StartCoroutine(OverClock());
    }
}
