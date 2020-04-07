using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energybolt : SkillInterface
{
    private new void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter in Energybolt");

        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("OnCollisionEnter(Enemy): " + collision.transform.name);
            collision.transform.GetComponent<IEnemy>().TakeDamage(Damage, this.transform);
            base.Extinguish();
        }
        //else if (collision.transform.name != "Ground")
        //{
        //    Extinguish();
        //}
    }
}
