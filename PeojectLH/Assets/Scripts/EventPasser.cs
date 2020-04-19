using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPasser : MonoBehaviour {
    public void SwordAttack()
    {
        GetComponentInParent<Player>().SwordAttack();
    }

    public void ShieldAttack()
    {
        GetComponentInParent<Player>().ShieldAttack();
    }
}
