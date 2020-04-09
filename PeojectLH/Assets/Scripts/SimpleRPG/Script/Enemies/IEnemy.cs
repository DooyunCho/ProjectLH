using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    string name { get; set; }
    Spawner Spawner { get; set; }
    int Experience { get; set; }
    void Die();
    void TakeDamage(int amount, Transform target);
    void PerformAttack();

    // event
    void GiveDamage();
}
