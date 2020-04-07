using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public Vector3 Direction { get; set; }
    public float Range { get; set; }
    public int Damage { get; set; }

    private int damage;

    Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(Direction * 100f);
    }

    private void Update()
    {
        if (Vector3.Distance(spawnPosition, transform.position) >= Range)
        {
            Extinguish();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<IEnemy>().TakeDamage(Damage, this.transform);
            Extinguish();
        }
        else if (collision.transform.name != "Ground")
        {
            Extinguish();
        }
    }

    void Extinguish()
    {
        Destroy(gameObject);
    }
}
