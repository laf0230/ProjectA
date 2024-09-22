using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }

    // Dictionary to store the ability mapping based on ID
    private Dictionary<int, System.Type> abilityMapping;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of AbilityManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: to keep it across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeAbilities();
    }

    // Initialize the ability mapping
    private void InitializeAbilities()
    {
        abilityMapping = new Dictionary<int, System.Type>
        {
            { 1, typeof(Invisible) }, // Example: 1 is the ID for Invisible ability
            {2, typeof(Poison) },
            // Add more mappings as necessary
        };

        Debug.Log("Ability Intialized");
    }

    // Method to get a list of abilities from abilityInfos
    public List<Ability_> GetAbilities(List<AbilityInfo> abilityInfos, MonoBehaviour mono)
    {
        List<Ability_> abilities = new List<Ability_>();
        DebugCreate(0);
            // 생성자에 mono 지역변수 전달

        foreach (var abilityInfo in abilityInfos)
        {
            Ability_ ability = CreateAbility(abilityInfo);
            if (ability != null)
            {
                ability.Info = abilityInfo; // Set ability info from SkillInfo
                ability.Initialize(abilityInfo, mono);
                abilities.Add(ability);
            }
        }

        Debug.Log(abilities != null ? "Success" : "Fail");

        return abilities;
    }

    // Method to instantiate the ability based on AbilityInfo
    private Ability_ CreateAbility(AbilityInfo abilityInfo)
    {
        Debug.Log($"Try Get Type From Mapping {abilityInfo.Name}");
        if (abilityMapping.TryGetValue(abilityInfo.ID, out System.Type abilityType))
        {
            Debug.Log("Secess From Get Mapping");
            return (Ability_)System.Activator.CreateInstance(abilityType);
        }
        else
        {
            Debug.LogWarning($"Ability with ID {abilityInfo.ID} not found in AbilityManager.");
            return null;
        }
    }

    private void DebugCreate(int type)
    {
        switch (type)
        {
            case 0:
                Debug.Log("Try Get Abilities...");
                break;
            case 1:
                Debug.Log("has Success");
                break;
            case 2:
                Debug.Log("has Fail");
                break;
        }
    }
}

