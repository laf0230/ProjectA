using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentUI_ : MonoBehaviour
{
    public List<GameObject> investedItemSlot; // 아이템을 넣을 슬롯 목록
    private Investment_ investment;
    [field: SerializeField] private Transform skillContainer;
    [field: SerializeField] private GameObject skillElementPrefab;
    [field: SerializeField] private TextMeshProUGUI investButtonText;
    [field: SerializeField] private TextMeshProUGUI name;
    [field: SerializeField] private TextMeshProUGUI health;
    [field: SerializeField] private TextMeshProUGUI additionalHealth;
    [field: SerializeField] private TextMeshProUGUI moveSpeed;
    [field: SerializeField] private TextMeshProUGUI additionalMoveSpeed;
    [field: SerializeField] private TextMeshProUGUI armorLevel;
    [field: SerializeField] private TextMeshProUGUI additionalArmorLevel;
    [field: SerializeField] private Button investCalcButton;
    [field: SerializeField] private GameObject investWarning;
    [field: SerializeField] private TextMeshProUGUI investCountText;
    private List<GameObject> skillSlots = new List<GameObject>();

    public void Start()
    {
        investment = GameManager_.instance.investment;
        investCalcButton.onClick.AddListener(OnInvestCalcButtonClick);
    }

    public void SetInvestCountText(string investCountText)
    {
        this.investCountText.text = investCountText;
    }

    public void OnInvestCalcButtonClick()
    {
        if (!investment.isInvested || investment.selectedCharacter.investData.isInvested)
        {
            // 투자하지 않은 상태에서만 투자 창이 열림
            UIManager_.Instance.investCalcUI.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(IEInvestWorning());
            return;
        }

    }

    public IEnumerator IEInvestWorning()
    {
        investWarning.SetActive(true);
        yield return new WaitForSeconds(1);
        investWarning.SetActive(false);
    }

    public void Initialize()
    {
        if(skillSlots != null)
            InitializeSkill();

        foreach (var item in investedItemSlot)
        {
            item.GetComponent<ItemSlotUI>().SetEmpty();
        }
    }

    public void UIUpdate()
    {
        Initialize();

        int slotCount = investedItemSlot.Count;

        for (int i = 0; i < slotCount; i++)
        {
            ItemSlotUI slotUI = investedItemSlot[i].GetComponent<ItemSlotUI>();

            if (!investment.selectedCharacter.investData.isInvested)
            {
                Debug.Log("투자는 진행되지 않았습니다.");
                // 잠긴 경우
                slotUI.isLock = true;
                slotUI.SetLock();
            }
            else
            {
                Debug.Log("투자가 진행되었습니다.");
                slotUI.isLock = false;
                // 아이템이 있는 경우에만 설정
                if (i < investment.items.Count)
                {
                    slotUI.SetItem(investment.items[i]);
                }
                else
                {
                    slotUI.SetEmpty();
                }
            }
        }
        if(investment.selectedCharacter != null)
        {
            SetCharacterInfomation(investment.selectedCharacter);

            // 스킬 목록 UI
            foreach (var skill in investment.selectedCharacter.Skills)
            {
                if (skill.Type != SkillType.Attack)
                {
                    AddSkill(skill);
                }
            }
        }
    }

    public void SetInvestBtnCurrencyAmount(int amount)
    {
        investButtonText.text = $"{amount} Chip";
    }

    private void SetCharacterInfomation(CharacterInfoSO characterInfo)
    {
        name.text = characterInfo.name;
        health.text = characterInfo.Status.MaxHealth.ToString();
        moveSpeed.text = characterInfo.Status.ChaseSpeed.ToString();
        armorLevel.text = characterInfo.Status.ArmorValue.ToString();

        #region 추가 스테이터스 출력

        // RectTransform의 Content Size Filter 컴포넌트 동작 도중 사이즈가 변할 경우 Refresh를 해야하는 버그 발생
        // 검색 키워드를 위해 영어로 작성

        additionalHealth.text = null;
        additionalMoveSpeed.text = null;
        additionalArmorLevel.text = null;

        Dictionary<StatusList, float> statusTotals = new Dictionary<StatusList, float>();

        foreach (var item in characterInfo.investData.investedItems)
        {
            foreach (var ability in item.Ability)
            {
                if (!statusTotals.ContainsKey(ability.EffectStatus))
                {
                    statusTotals[ability.EffectStatus] = 0f;
                }

                // EffectValue를 각 상태에 더함
                statusTotals[ability.EffectStatus] += ability.EffectValue;
            }
        }

        // 최종 상태를 출력
        foreach (var status in statusTotals)
        {
            SetAdditionalInfomation(status.Key, status.Value);
        }

        Debug.Log("랙트가 리로딩됩니다!");

        var statusUIContainer = UIManager_.Instance.rect;
        statusUIContainer.sizeDelta = new Vector2(statusUIContainer.sizeDelta.x + 1, statusUIContainer.sizeDelta.y);

        LayoutRebuilder.ForceRebuildLayoutImmediate(additionalHealth.transform.parent as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(additionalMoveSpeed.transform.parent as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(additionalArmorLevel.transform.parent as RectTransform);

        #endregion
    }

    public void SetAdditionalInfomation(StatusList status, float value)
    {
        // 아 아아아아앙아가ㅓㅇㄴ미ㅏㅁ런이ㅏ;럼니아ㅓ리;ㅁㄴ어리;ㅁ나아ㅏㅓㅏ라ㅏㅏㅏㅣ;만아ㅏㅏㅓ라ㅏㅁㄴ;ㅣ
        // 일단 1. 보유하고 있는 아이템의 총 합을 얻어야해
        // 2. 총 합을 적용
        // 총 합을 어떻게 가져오지
        // 어엄,,,,,,,,,,,,,,,,,,,,,,,,,,,,,..........................................
        // ..................................................................................
        // ...................................................
        // ㅓㄹㅇㅁ니ㅏ러ㅣㅁ;너ㅏ림너리;ㄴ마ㅓㄻ;ㄴ
        string plusText = value > 0 ? " +" : " -";

        switch (status)
        {
            case StatusList.Health:
                additionalHealth.text = plusText + value.ToString();
                break;
            case StatusList.Speed:
                additionalMoveSpeed.text = plusText + value.ToString();
                break;
                case StatusList.Armor:
                additionalArmorLevel.text = plusText + value.ToString();
                break;
            default:
                Debug.LogError($"{status} 수치는 공간이 없습니다!");
                break;
        }
    }

    private void AddSkill(SkillSO skillSO)
    {
        var skillObject = Instantiate(skillElementPrefab, skillContainer);
        var skillObjectTMP = skillObject.GetComponentInChildren<TextMeshProUGUI>();

        skillObjectTMP.text = skillSO.Profile.Name;
        skillSlots.Add(skillObject);
    }

    private void InitializeSkill()
    {
        foreach (var item in skillSlots)
        {
            Destroy(item);
        }
        skillSlots.Clear();
    }
}
