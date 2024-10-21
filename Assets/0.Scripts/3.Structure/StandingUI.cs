using UnityEngine;
using UnityEngine.UI;

public class StandingUI: MonoBehaviour
{
    private Image fullIllust;

    private void Start()
    {
        fullIllust = GetComponent<Image>();
    }

    public void SetIllust(Sprite fullIllust)
    {
        this.fullIllust.sprite = fullIllust;
    }
}

