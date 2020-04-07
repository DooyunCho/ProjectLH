using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slim : MonoBehaviour, IEnemy {
    public LayerMask aggroLayerMask;
    public float currentHealth, power, toughness;
    public float maxHealth;
    public int Experience { get; set; }
    public DropTable DropTable { get; set; }
    public Spawner Spawner { get; set; }
    public PickupItem pickupItem;
    private float aggroRange = 10f;

    private Player player;
    private NavMeshAgent navAgent;
    private CharacterStats characterStats;
    private Collider[] withInAggroColliders;

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
        navAgent = GetComponent<NavMeshAgent>();
        characterStats = new CharacterStats(6, 10, 2, 4);
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        withInAggroColliders = Physics.OverlapSphere(transform.position, aggroRange, aggroLayerMask);

        if (withInAggroColliders.Length > 0)
        {
            ChasePlayer(withInAggroColliders[0].GetComponent<Player>());
        }
        else if(player != null)
        {
            navAgent.SetDestination(player.transform.position);
            CancelInvoke("PerformAttack");
        }
    }
    
	public void PerformAttack () {
        player.TakeDamage(5);
	}
    
    public void TakeDamage (int amount, Transform target) {
        transform.LookAt(target);
        Debug.Log(this.name + " take " + amount + " damage.");
        aggroRange = 20;
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ChasePlayer(Player player)
    {
        this.player = player;
        navAgent.SetDestination(player.transform.position);

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", 2f, 2.5f);
            }
        }
        else
        {
            CancelInvoke("PerformAttack");
        }
    }

    public void Die()
    {
        //DropLoot();
        CombatEvents.EnemyDied(this);
        //this.Spawner.Respawn();
        Destroy(gameObject);
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
