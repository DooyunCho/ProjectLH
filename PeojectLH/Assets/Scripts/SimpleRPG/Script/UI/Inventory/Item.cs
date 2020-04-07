using System.Collections.Generic;

public class Item{
    public enum ItemTypes { Equipment, Consumable, Quest}
    public List<BaseStat> Stats { get; set; }
    public float AttackRange { get; set; }
    public string ObjectSlug { get; set; }
    public string Description { get; set; }
    public string ActionName { get; set; }
    public string ItemName { get; set; }
    public bool ItemModifier { get; set; }
    public string WeaponType { get; set; }
    
    public ItemTypes itemType { get; set; }

    public Item(List<BaseStat> _Stats, string _ObjectSlug)
    {
        this.Stats = _Stats;
        this.ObjectSlug = _ObjectSlug;
    }
    
    public Item(List<BaseStat> _Stats, string _ObjectSlug, string _Description, ItemTypes _ItemType, string _WeaponType, float _AttackRange, string _ActionName, string _ItemName, bool _ItemModifier)
    {
        this.Stats = _Stats;
        this.ObjectSlug = _ObjectSlug;
        this.Description = _Description;
        this.itemType = _ItemType;
        this.WeaponType = _WeaponType;
        this.AttackRange = _AttackRange;
        this.ActionName = _ActionName;
        this.ItemName = _ItemName;
        this.ItemModifier = _ItemModifier;
    }
}
