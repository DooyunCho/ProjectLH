using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {
    public int Level { get; set; }
    public int CurrentExperience { get; set; }
    public int RequiredExperience { get { return Level * 25; } }

	// Use this for initialization
	void Start () {
        CombatEvents.OnEnemyDeath += EnemyToExperience;
        Level = 1;
	}

    public void EnemyToExperience(IEnemy enemy)
    {
        GrantExperience(enemy.Experience);
    }

    public void GrantExperience(int amount)
    {
        CurrentExperience += amount;

        while (CurrentExperience >= RequiredExperience)
        {
            CurrentExperience -= RequiredExperience;
            Level++;
            GetComponent<Player>().MaxHealth = (int)System.Math.Round(GetComponent<Player>().MaxHealth * 1.2f);
            GetComponent<Player>().MaxMana = (int)System.Math.Round(GetComponent<Player>().MaxMana * 1.2f);

            GetComponent<Player>().recoveryHealth(GetComponent<Player>().MaxHealth);
            GetComponent<Player>().recoveryMana(GetComponent<Player>().MaxMana);
        }

        UIEventHandler.PlayerLevelChanged();
    }
}
