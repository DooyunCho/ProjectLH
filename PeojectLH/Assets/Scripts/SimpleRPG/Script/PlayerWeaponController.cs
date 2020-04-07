using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWeaponController : MonoBehaviour {
    public GameObject playerHand;
    public GameObject EquippedWeaponObject { get; set; }

    Transform spawnProjectile;
    Item currentlyEquippedItem;
    IWeapon equippedWeapon;
    CharacterStats characterStats;

    void Start()
    {
        spawnProjectile = transform.Find("ProjectileSpawn");
        characterStats = GetComponent<Player>().characterStats;
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeaponObject != null)
        {
            Debug.Log("UnequippedWeapon");
            UnequipWeapon();
        }

        EquippedWeaponObject = Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), 
            playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon = EquippedWeaponObject.GetComponent<IWeapon>();

        if (EquippedWeaponObject.GetComponent<IProjectileWeapon>() != null)
        {
            if (spawnProjectile == null) Debug.Log("SpawnProjectile is null");
            EquippedWeaponObject.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        }

        EquippedWeaponObject.transform.SetParent(playerHand.transform);
        EquippedWeaponObject.name = itemToEquip.WeaponType;
        EquippedWeaponObject.GetComponent<BoxCollider>().enabled = false;
        equippedWeapon.CharacterStats = characterStats;
        equippedWeapon.Stats = itemToEquip.Stats;
        equippedWeapon.AttackRange = itemToEquip.AttackRange;
        currentlyEquippedItem = itemToEquip;
        characterStats.AddStatBonus(itemToEquip.Stats);
        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatChanged();

        AnimationController.Instance.Rebind();
    }

    public void UnequipWeapon()
    {
        if (EquippedWeaponObject != null)
        {
            InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
            characterStats.RemoveStatBonus(EquippedWeaponObject.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
            EquippedWeaponObject = null;
            UIEventHandler.StatChanged();
        }
    }
    
    public void PerformWeaponAttack()
    {
        equippedWeapon.PerformAttack(CalculateDamage());

        AnimationController.Instance.setTrigger("Base_Attack", true);

        // move front a little.
        //NavMeshAgent playerAgent = this.GetComponent<NavMeshAgent>();
        //playerAgent.updatePosition = true;
        //gameObject.transform.position += (gameObject.transform.forward * Time.deltaTime * 10);
        //playerAgent.updatePosition = false;
    }

    public void PerformWeaponAttack(GameObject enemy)
    {
        NavMeshAgent playerAgent = this.GetComponent<NavMeshAgent>();
        playerAgent.stoppingDistance = EquippedWeaponObject.GetComponent<IWeapon>().AttackRange;
        
        if (Vector3.Distance(enemy.transform.position, transform.position) > EquippedWeaponObject.GetComponent<IWeapon>().AttackRange)
        {
            playerAgent.destination = enemy.transform.position;
        }
        else
        {
            EnsureLookDirection(enemy);
            PerformWeaponAttack();

            AnimationController.Instance.setTrigger("Base_Attack", true);
        }
    }

    public void PerformWeaponSpecialAttack()
    {
        equippedWeapon.PerformSpecialAttack();
    }

    private int CalculateDamage()
    {
        int damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue() * 2) +
            Random.Range(2, 8);
        damageToDeal += CalculateCrit(damageToDeal);
        return damageToDeal;
    }

    private int CalculateCrit(int damage)
    {
        // 10%
        if (Random.value <= 0.10f)
        {
            int critDamage = (int)(damage * Random.Range(0.25f, 0.5f));
            return critDamage;
        }

        return 0;
    }

    void EnsureLookDirection(GameObject target)
    {
        NavMeshAgent playerAgent = this.GetComponent<NavMeshAgent>();
        playerAgent.updateRotation = false;
        Vector3 lookDirection = new Vector3(target.transform.position.x, playerAgent.transform.position.y, target.transform.position.z);
        playerAgent.transform.LookAt(lookDirection);
        playerAgent.updateRotation = true;
    }
}
