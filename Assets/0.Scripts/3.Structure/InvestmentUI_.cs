using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentUI_ : MonoBehaviour
{
    public List<GameObject> investedItemSlot; // �������� ���� ���� ���
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
            // �������� ���� ���¿����� ���� â�� ����
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
                Debug.Log("���ڴ� ������� �ʾҽ��ϴ�.");
                // ��� ���
                slotUI.isLock = true;
                slotUI.SetLock();
            }
            else
            {
                Debug.Log("���ڰ� ����Ǿ����ϴ�.");
                slotUI.isLock = false;
                // �������� �ִ� ��쿡�� ����
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

            // ��ų ��� UI
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

        #region �߰� �������ͽ� ���

        // RectTransform�� Content Size Filter ������Ʈ ���� ���� ����� ���� ��� Refresh�� �ؾ��ϴ� ���� �߻�
        // �˻� Ű���带 ���� ����� �ۼ�

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

                // EffectValue�� �� ���¿� ����
                statusTotals[ability.EffectStatus] += ability.EffectValue;
            }
        }

        // ���� ���¸� ���
        foreach (var status in statusTotals)
        {
            SetAdditionalInfomation(status.Key, status.Value);
        }

        Debug.Log("��Ʈ�� ���ε��˴ϴ�!");

        var statusUIContainer = UIManager_.Instance.rect;
        statusUIContainer.sizeDelta = new Vector2(statusUIContainer.sizeDelta.x + 1, statusUIContainer.sizeDelta.y);

        LayoutRebuilder.ForceRebuildLayoutImmediate(additionalHealth.transform.parent as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(additionalMoveSpeed.transform.parent as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(additionalArmorLevel.transform.parent as RectTransform);

        #endregion
    }

    public void SetAdditionalInfomation(StatusList status, float value)
    {
        // �� �ƾƾƾƾӾư��ä����̤������̤�;���ϾƤø�;�����;�����Ƥ��ä��󤿤�����;���Ƥ����ö󤿤���;��
        // �ϴ� 1. �����ϰ� �ִ� �������� �� ���� ������
        // 2. �� ���� ����
        // �� ���� ��� ��������
        // ���,,,,,,,,,,,,,,,,,,,,,,,,,,,,,..........................................
        // ..................................................................................
        // ...................................................
        // �ä������Ϥ����Ӥ�;�ʤ����ʸ�;�����ä�;��
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
                Debug.LogError($"{status} ��ġ�� ������ �����ϴ�!");
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
