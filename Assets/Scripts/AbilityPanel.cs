using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    public Player Player;
    public Weapon Weapon;
    public List<GameObject> AbilitiesPrefabs;
    public List<GameObject> Abilities;

    void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, AbilitiesPrefabs.Count);
            GameObject abilityPrefab = AbilitiesPrefabs[randomIndex];
            GameObject abilityInstance = Instantiate(abilityPrefab, transform);
            abilityInstance.TryGetComponent<MoveSpeed>(out var moveSpeed);
            abilityInstance.TryGetComponent<MaxHealth>(out var maxHealth);
            abilityInstance.TryGetComponent<FireRate>(out var fireRate);
            if (moveSpeed != null)
            {
                moveSpeed.Player = Player;
            }
            if (maxHealth != null)
            {
                maxHealth.Player = Player;
            }
            if (fireRate != null)
            {
                fireRate.Player = Player;
                fireRate.Weapon = Weapon;
            }
            Abilities.Add(abilityInstance);
        }
    }

    void OnDisable()
    {
        // destroy all abilities
        foreach (var ability in Abilities)
        {
            Destroy(ability);
        }
    }

}