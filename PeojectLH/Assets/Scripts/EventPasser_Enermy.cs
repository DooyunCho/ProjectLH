using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPasser_Enermy : MonoBehaviour {
    public void SwordAttack()
    {
        GetComponentInParent<IEnemy>().GiveDamage();
    }
}
