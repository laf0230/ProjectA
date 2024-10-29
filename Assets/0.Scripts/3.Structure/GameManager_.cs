using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager_ instance;

    // �÷��̾� ������
    public Player_ player = new Player_();
    public List<CardSO> cards = new List<CardSO>();
    public List<Character> selectedCharacters;
    public List<CardSO> selectedCards;
    public Inventory_ inventory;
    public Shop_ shop;
    public Investment_ investment;

    [Header("���� ����")]
    public int characterCount = 1;

    // �̱��� �ʱ�ȭ
    private void Awake()
    {
        Debug.Log(shop);
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
        GameStart();
    }

    // ���� ���� �޼���
    public void StartBattle()
    {
        // 'Character' �±׸� ���� ��� ������Ʈ�� Ȱ��ȭ
        player.inventory.Initialize();

        foreach (var item in selectedCharacters)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void GameStart()
    {
        selectedCards = SelectRandomCharacters();
        // UIManager_.Instance.cardContainer.gameObject.SetActive(true);
        UIManager_.Instance.cardContainer.SetCards(selectedCards);
    }

    public List<CardSO> SelectRandomCharacters()
    {
        var selectedCards = new List<CardSO>();
        for(int i = 0; i < characterCount; i++)
        {
            var selectedCard = cards[Random.Range(0, cards.Count - 1)];

            selectedCards.Add(selectedCard);
        }
        return selectedCards;
    }

    public void InstantiateCharacter(CardSO cardSO, Vector3 position)
    {
        // ĳ������ ���� �� ��ġ �ڵ� �ۼ�
    }
}

