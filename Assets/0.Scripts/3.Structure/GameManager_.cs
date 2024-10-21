using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ : MonoBehaviour
{
    // 싱글턴 인스턴스
    public static GameManager_ instance;

    // 플레이어 데이터
    public Player_ player = new Player_();
    public List<Character> characters;

    // 싱글턴 초기화
    private void Awake()
    {
        // 인스턴스가 존재하지 않는 경우 현재 인스턴스로 설정
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 파괴
        }
    }

    private void Start()
    {
        player.Gold = 10000;
        player.Chip = 10000;

        // UI 업데이트
        UIManager_.Instance.UpdateCurrencyUI();
    }

    // 전투 시작 메서드
    public void StartBattle()
    {
        // 'Character' 태그를 가진 모든 오브젝트를 활성화
        foreach (var item in characters)
        {
            item.gameObject.SetActive(true);
        }
    }
}

