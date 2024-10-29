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
    public Inventory_ inventory;
    public Shop_ shop;
    public Investment_ investment;

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
        player.gold.amount = 10000;
        player.gold.amount = 10000;

        // UI ������Ʈ
        UIManager_.Instance.UpdateCurrencyUI();

        inventory.Initialize();
        shop.Initialize();
    }

    // ���� ���� �޼���
    public void StartBattle()
    {
        // 'Character' �±׸� ���� ��� ������Ʈ�� Ȱ��ȭ
        player.inventory.Initialize();

        foreach (var item in characters)
        {
            item.gameObject.SetActive(true);
        }
    }
}

