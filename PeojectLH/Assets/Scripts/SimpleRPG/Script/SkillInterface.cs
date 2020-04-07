using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkillInterface : MonoBehaviour {
    public Vector3 Direction { get; set; }
    public float Range { get; set; }
    public int Damage { get; set; }
    public GameObject Target { get; set; }
    private int damage;
    Vector3 spawnPosition;

    public float speed = 1f;
    public float acceleration = 1f;

    private float currentSpeed;
    private float targetSpeed = 20f;

    void Start()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

        if (Target)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * currentSpeed);
        }
        else
        {
            Debug.Log("Lost target");
            transform.Translate(transform.forward * Time.deltaTime * currentSpeed);
        }

        if (Vector3.Distance(spawnPosition, transform.position) >= Range)
        {
            Extinguish();
        }
    }

    protected float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            //방향 Sign -> 음수 면 - 1 양수거나 0이면 1 반환
            float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("OnCollisionEnter(Enemy): " + collision.transform.name);
            collision.transform.GetComponent<IEnemy>().TakeDamage(Damage, this.transform);
            Extinguish();
        }
        //else if (collision.transform.name != "Ground")
        //{
        //    Extinguish();
        //}
    }

    protected void Extinguish()
    {
        Destroy(gameObject);
    }
}
