using System.Collections.Generic;
using UnityEngine;

public interface IWeapon{
    float AttackRange { get; set; }
    List<BaseStat> Stats { get; set; }
    int CurrentDamage { get; set; }
    CharacterStats CharacterStats { get; set; }
    void PerformAttack(int damage);
    void PerformSpecialAttack();
    void Attack(Collider col);
}
