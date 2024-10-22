using UnityEngine;
using UnityEngine.UI;

public class StandingUI: MonoBehaviour
{
    [SerializeField] private Image fullIllust;
    public void SetIllust(Sprite fullIllust)
    {
        this.fullIllust.sprite = fullIllust;
    }
}

