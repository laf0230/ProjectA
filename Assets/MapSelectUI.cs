using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectUI : UI
{
    public Sprite selectedWrold;

    public void SetWorldImage(Sprite sprite)
    {
        selectedWrold = sprite;
    }
}
