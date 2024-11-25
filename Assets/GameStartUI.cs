using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private Animator mainUIAnim;
    [SerializeField] private Button mainUIContainer;
    [SerializeField] private Image gameStartMenuImage;
    [SerializeField] private float blinkDuration = 1f; // 깜빡거림 주기

    private bool isBlinking = false; // 깜빡거림 활성화 여부

    // 주문 내용
    /*
    1. 시작 화면 버튼 깜빡
    2. 어떤 버튼이든 클릭 시 애니메이션 실행
    3. 애니메이션이 종료되면 UI 등장
     */

    private void Start()
    {
        ActiveMainAnim(false);
        StopBlinking();
    }

    private void Update()
    {
        ActiveMainAnim(true);
    }

    public void ActiveMainAnim(bool isActive)
    {
        mainUIAnim.enabled = isActive;
    }

    #region Blinking Start Image

    public void StartBlinking()
    {
        gameStartMenuImage.gameObject.SetActive(true);
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkCoroutine());
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
        gameStartMenuImage.transform.parent.gameObject.SetActive(false);
    }

    // 버튼과 함게 사용
    public void ActiveMainUI(bool isActive)
    {
        mainUIContainer.transform.parent.gameObject.SetActive(isActive);
    }

    private IEnumerator BlinkCoroutine()
    {
        while (isBlinking)
        {
            // 투명도 감소
            yield return Fade(1f, 0f, blinkDuration / 2);
            if (!isBlinking) yield break;

            // 투명도 증가
            yield return Fade(0f, 1f, blinkDuration / 2);
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = gameStartMenuImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            gameStartMenuImage.color = color;
            yield return null;
        }
        color.a = endAlpha;
        gameStartMenuImage.color = color;
    }

    #endregion


}
