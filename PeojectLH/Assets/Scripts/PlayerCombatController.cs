using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    void Update () {
        if (Input.GetMouseButtonDown(0)) // Mouse left Click
        {
            PerformWeaponAttack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            AnimationController.Instance.setBool("Guard", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            AnimationController.Instance.setBool("Guard", false);
        }
    }
    
    public void PerformWeaponAttack()
    {
        AnimationController.Instance.setTrigger("Attack", true);
    }
}
