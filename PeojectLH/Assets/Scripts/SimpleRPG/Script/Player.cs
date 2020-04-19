using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public CharacterStats characterStats;
    public int CurrentHealth;
    public int MaxHealth;
    public int CurrentMana;
    public int MaxMana;
    public PlayerLevel playerLevel { get; set; }
    private ObjectGetter objectGetter;

    void Start()
    {
        playerLevel = GetComponent<PlayerLevel>();
        recoveryHealth(this.MaxHealth);
        recoveryMana(this.MaxMana);
        UIEventHandler.PlayerLevelChanged();
        characterStats = new CharacterStats(10, 10, 10, 10);
        
        objectGetter = transform.GetChild(2).GetComponent<ObjectGetter>();
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Player takes: " + amount + " damage!");
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
        //UIEventHandler.HealthChanged(this.CurrentHealth, this.MaxHealth);
        AnimationController.Instance.setTrigger("Hit", true);
    }

    public void recoveryHealth(int amount)
    {
        if (CurrentHealth + amount >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += amount;
        }
        UIEventHandler.HealthChanged(this.CurrentHealth, this.MaxHealth);
    }

    public void recoveryMana(int amount)
    {
        if (CurrentMana + amount >= MaxMana)
        {
            CurrentMana = MaxMana;
        }
        else
        {
            CurrentMana += amount;
        }
        UIEventHandler.ManaChanged(this.CurrentMana, this.MaxMana);
    }

    private void Die()
    {
        Debug.Log("Player dead. Reset health.");
        this.CurrentHealth = this.MaxHealth;
        UIEventHandler.HealthChanged(this.CurrentHealth, this.MaxHealth);
    }

    public void SwordAttack()
    {
        if (objectGetter == null)
            objectGetter = transform.GetChild(2).GetComponent<ObjectGetter>();

        List<Collider> targetObjects = objectGetter.GetComponent<ObjectGetter>().GetColliders();

        for (int i = 0; i < targetObjects.Count; i++)
        {
            Collider collision = targetObjects[i];
            collision.transform.GetComponent<IEnemy>().TakeDamage(40, this.transform);
        }
    }

    public void ShieldAttack()
    {
        if (objectGetter == null)
            objectGetter = transform.GetChild(2).GetComponent<ObjectGetter>();

        List<Collider> targetObjects = objectGetter.GetComponent<ObjectGetter>().GetColliders();

        for (int i = 0; i < targetObjects.Count; i++)
        {
            Collider collision = targetObjects[i];
            collision.transform.GetComponent<IEnemy>().TakeDamage(10, this.transform);
        }
    }
}
