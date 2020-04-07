using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public float AttackRange { get; set; }
    public List<BaseStat> Stats { get; set; }
    public CharacterStats CharacterStats { get; set; }
    public int CurrentDamage { get; set; }

    void Start()
    {
        //GetComponent<BoxCollider>().enabled = false;
    }

    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;
    }

    public void PerformSpecialAttack()
    {
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hit: " + col.name);
        if (col.tag == "Enemy")
        {
            Attack(col);
        }
    }

    public void Attack(Collider col)
    {
        col.GetComponent<IEnemy>().TakeDamage(CharacterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue(), this.transform);
    }

    public void CastProjectile()
    {
        // do nothing
    }
}
