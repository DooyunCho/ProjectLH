using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour {
    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInventory;
    public static event ItemEventHandler OnItemEquipped;

    public delegate void SkillEventHandler(Skill skill);
    public static event SkillEventHandler OnSkillAdded;

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void PlayerManaEventHandler(int currentMana, int maxMana);
    public static event PlayerManaEventHandler OnPlayerManaChanged;

    public delegate void StatsChangeEventHandler();
    public static event StatsChangeEventHandler OnStatsChanged;

    public delegate void PlayerLevelEventHandler();
    public static event PlayerLevelEventHandler OnPlayerLevelChanged;

    public static void ItemAddedToInventory(Item item)
    {
        OnItemAddedToInventory(item);
    }

    public static void ItemEquipped(Item item)
    {
        OnItemEquipped(item);
    }

    public static void SkillAdded(Skill skill)
    {
        OnSkillAdded(skill);
    }

    public static void HealthChanged(int currentHealth, int maxHealth)
    {
        OnPlayerHealthChanged(currentHealth, maxHealth);
    }

    public static void ManaChanged(int currentMana, int maxMana)
    {
        OnPlayerManaChanged(currentMana, maxMana);
    }

    public static void StatChanged()
    {
        OnStatsChanged();
    }
    
    public static void PlayerLevelChanged()
    {
        OnPlayerLevelChanged();
    }
}
