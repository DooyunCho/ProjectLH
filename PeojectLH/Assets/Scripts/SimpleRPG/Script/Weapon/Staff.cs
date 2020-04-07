using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon, IProjectileWeapon
{
    public float AttackRange { get; set; }
    public List<BaseStat> Stats { get; set; }
    public CharacterStats CharacterStats { get; set; }
    public Transform ProjectileSpawn { get; set; }
    public int CurrentDamage{ get; set; }

    Energybolt energybolt;

    void Start()
    {
        energybolt = Resources.Load<Energybolt>("Weapons/Projectiles/Snowball");
    }

    public void PerformAttack(int damage)
    {
    }

    public void PerformSpecialAttack()
    {
    }

    public void CastProjectile()
    {
        Energybolt spelllInstance = (Energybolt)Instantiate(energybolt, ProjectileSpawn.position, ProjectileSpawn.rotation);
        spelllInstance.Direction = ProjectileSpawn.forward;
        spelllInstance.Damage = 4;
        spelllInstance.Range = 10;
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
}
