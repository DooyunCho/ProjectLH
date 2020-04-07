using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats {
    public List<BaseStat> stats = new List<BaseStat>();
    
    public CharacterStats(int power, int toughness, int attackSpeed, int Intelligence)
    {
        stats = new List<BaseStat>()
        {
            new BaseStat(BaseStat.BaseStatType.Power, power, "Power"),
            new BaseStat(BaseStat.BaseStatType.Toughness, toughness, "Toughness"),
            new BaseStat(BaseStat.BaseStatType.AttackSpeed, attackSpeed, "Atk Spd"),
            new BaseStat(BaseStat.BaseStatType.Intelligence, Intelligence, "Intel")
        };
    }

    public BaseStat GetStat(BaseStat.BaseStatType statType)
    {
        return this.stats.Find(x => x.StatType == statType);

    }

    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach(BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
}
