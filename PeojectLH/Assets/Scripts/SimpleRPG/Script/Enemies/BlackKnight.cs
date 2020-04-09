using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackKnight: MonoBehaviour, IEnemy {
    public LayerMask aggroLayerMask;
    public float currentHealth, power, toughness;
    public float maxHealth;
    public int Experience { get; set; }
    public DropTable DropTable { get; set; }
    public Spawner Spawner { get; set; }
    public PickupItem pickupItem;
    public float speed;
    private float aggroRange = 10f;
    private float attackCoolTime = 1f;

    private Player player;
    private CharacterStats characterStats;
    private Collider[] withInAggroColliders;

    private Animator animator;

    void Start()
    {
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("sword", 25),
            new LootDrop("staff", 25),
            new LootDrop("potion_log", 40)
        };

        Experience = 20;
        characterStats = new CharacterStats(6, 10, 2, 4);
        currentHealth = maxHealth;

        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        withInAggroColliders = Physics.OverlapSphere(transform.position, aggroRange, aggroLayerMask);

        if (withInAggroColliders.Length > 0)
        {
            player = withInAggroColliders[0].GetComponent<Player>();

            if (Vector3.Distance(transform.position, player.transform.position) <= 2.0f)
            {
                animator.SetFloat("Vertical", 0.0f);

                if (!IsInvoking("PerformAttack"))
                {
                    InvokeRepeating("PerformAttack", attackCoolTime, 2.5f);
                }
            }
            else
            {
                CancelInvoke("PerformAttack");
                ChasePlayer(player.GetComponent<Player>());
            }
        }
        else if(player != null)
        {
            CancelInvoke("PerformAttack");
        }
        else
        {
            animator.SetFloat("Vertical", 0.0f);
        }
    }
    
	public void PerformAttack ()
    {
        animator.SetTrigger("Attack");
	}

    public void GiveDamage()
    {
        Debug.Log(this.name + " give " + 5 + " damage.");
        player.TakeDamage(5);
    }
    
    public void TakeDamage (int amount, Transform target)
    {
        Debug.Log(this.name + " take " + amount + " damage.");

        transform.LookAt(target);
        aggroRange = 20;
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    void ChasePlayer(Player player)
    {
        Vector3 wayPointPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        
        transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
        transform.LookAt(player.transform);

        animator.SetFloat("Vertical", 1.0f);
    }

    public void Die()
    {
        //DropLoot();
        CombatEvents.EnemyDied(this);
        animator.SetTrigger("Die");
        aggroRange = 0.0f;
        //this.Spawner.Respawn();
        //Destroy(gameObject);
    }

    void DropLoot()
    {
        Item item = DropTable.GetDrop();

        if (item != null)
        {
            PickupItem instance = Instantiate<PickupItem>(pickupItem, transform.position, Quaternion.identity);
            instance.itemDrop = item;
        }
    }
}
