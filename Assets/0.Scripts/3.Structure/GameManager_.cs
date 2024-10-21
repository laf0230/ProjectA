using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager_ instance;

    // �÷��̾� ������
    public Player_ player = new Player_();
    public List<Character> characters;

    // �̱��� �ʱ�ȭ
    private void Awake()
    {
        // �ν��Ͻ��� �������� �ʴ� ��� ���� �ν��Ͻ��� ����
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ı�
        }
    }

    private void Start()
    {
        player.Gold = 10000;
        player.Chip = 10000;

        // UI ������Ʈ
        UIManager_.Instance.UpdateCurrencyUI();
    }

    // ���� ���� �޼���
    public void StartBattle()
    {
        // 'Character' �±׸� ���� ��� ������Ʈ�� Ȱ��ȭ
        foreach (var item in characters)
        {
            item.gameObject.SetActive(true);
        }
    }
}

