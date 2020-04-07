using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public enum SkillTypes { Attack, Heal, Buff, Debuff }
    public enum ElementTypes { Fire, Water, Earth, Wind, Thunder }
    public string SkillSlug { get; set; }
    public string SkillName { get; set; }
    public string Description { get; set; }
    public List<BaseStat> Stats { get; set; }
    public float SkillRange { get; set; }
    public float SkillDistance { get; set; }
    
    public SkillTypes SkillType { get; set; }
    public ElementTypes ElementType { get; set; }

    public Skill(List<BaseStat> _Stats, string _SkillSlug)
    {
        this.Stats = _Stats;
        this.SkillSlug = _SkillSlug;
    }
    
    public Skill(List<BaseStat> _Stats, string _SkillSlug, string _Description, SkillTypes _SkillType, string _SkillName, float _SkillRange, float _SkillDistance, ElementTypes _ElementType)
    {
        this.Stats = _Stats;
        this.SkillSlug = _SkillSlug;
        this.Description = _Description;
        this.SkillType = _SkillType;
        this.SkillName = _SkillName;
        this.SkillRange = _SkillRange;
        this.SkillDistance = _SkillDistance;
        this.ElementType = _ElementType;
    }
}
