using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private Animator mainUIAnim;
    [SerializeField] private Button mainUIContainer;
    [SerializeField] private Image gameStartMenuImage;
    [SerializeField] private float blinkDuration = 1f; // �����Ÿ� �ֱ�

    private bool isBlinking = false; // �����Ÿ� Ȱ��ȭ ����

    // �ֹ� ����
    /*
    1. ���� ȭ�� ��ư ����
    2. � ��ư�̵� Ŭ�� �� �ִϸ��̼� ����
    3. �ִϸ��̼��� ����Ǹ� UI ����
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

    // ��ư�� �԰� ���
    public void ActiveMainUI(bool isActive)
    {
        mainUIContainer.transform.parent.gameObject.SetActive(isActive);
    }

    private IEnumerator BlinkCoroutine()
    {
        while (isBlinking)
        {
            // ���� ����
            yield return Fade(1f, 0f, blinkDuration / 2);
            if (!isBlinking) yield break;

            // ���� ����
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
