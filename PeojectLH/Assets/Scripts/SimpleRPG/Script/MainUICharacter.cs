using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUICharacter : MonoBehaviour
{
    [SerializeField] private Text health, mana, exp, level;
    [SerializeField] private Image healthFill, manaFill, expFill;
    [SerializeField] private Player player;

    // Use this for initialization
    void Awake ()
    {
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnPlayerManaChanged += UpdateMana;
        UIEventHandler.OnPlayerLevelChanged += UpdateLevel;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void UpdateHealth(int currentHealth, int maxHealth)
    {
        this.health.text = currentHealth.ToString() + "/" + maxHealth;
        this.healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    void UpdateMana(int currentMana, int maxMana)
    {
        this.mana.text = currentMana.ToString() + "/" + maxMana;
        this.manaFill.fillAmount = (float)currentMana / (float)maxMana;
    }

    void UpdateLevel()
    {
        this.level.text = player.playerLevel.Level.ToString();
        this.exp.text = player.playerLevel.CurrentExperience.ToString() + "/" + player.playerLevel.RequiredExperience;
        this.expFill.fillAmount = (float)player.playerLevel.CurrentExperience / (float)player.playerLevel.RequiredExperience;
    }
}
