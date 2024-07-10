using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStrikingCheck : MonoBehaviour
{
    // Attack Target
    public List<GameObject> Targets { get; set; }
    private Character _character;

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
        Targets = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _character.gameObject && other.CompareTag("Character"))
        {
            if (!other.enabled) return;

            Targets.Add(other.gameObject);
            _character.SetStrikingDistance(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _character.gameObject && other.CompareTag("Character"))
        {
            Targets.Remove(other.gameObject);
            _character.SetStrikingDistance(false);
        }
    }
}
