using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackController : MonoBehaviour
{
    private Animator animator;
    private ObjectGetter objectGetter;

    void Start()
    {
        animator = transform.GetChild(1).GetComponent<Animator>();
        objectGetter = transform.GetChild(2).GetComponent<ObjectGetter>();
    }
    
    void Update () {
        if (Input.GetMouseButtonDown(0)) // Mouse left Click
        {
            PerformWeaponAttack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("Guard", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("Guard", false);
        }
    }
    
    public void PerformWeaponAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void attack()
    {
        List<Collider> targetObjects = objectGetter.GetColliders();

        for(int i = 0; i < targetObjects.Count; i++)
        {
            Collider collision = targetObjects[i];
            collision.transform.GetComponent<IEnemy>().TakeDamage(40, this.transform);
        }
    }
}
