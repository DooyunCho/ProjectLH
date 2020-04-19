using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            AnimationController.Instance.setBool("Guard", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            AnimationController.Instance.setBool("Guard", false);
        }

        if (Input.GetMouseButtonDown(0)) // Mouse left Click
        {
            // 가드 체크
            if (AnimationController.Instance.getBool("Guard"))
            {
                PerformSheildAttack();
            }
            else
            {
                PerformWeaponAttack();
            }
        }
    }
    
    public void PerformWeaponAttack()
    {
        AnimationController.Instance.setTrigger("SwordAttack", true);
    }

    public void PerformSheildAttack()
    {
        AnimationController.Instance.setTrigger("ShieldAttack", true);
    }
}
