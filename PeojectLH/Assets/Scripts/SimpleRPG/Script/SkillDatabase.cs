using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour {
    public static SkillDatabase Instance { get; set; }
    private List<Skill> Skills { get; set; }

    // Use this for initialization
    void Awake()
    {
        Debug.Log("SkillDatabase Awake");
        if (Instance != null && Instance != this)
        {
            Debug.Log("SkillDatabase Destroy");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("SkillDatabase Instance");
            Instance = this;
        }

        BuildDatabase();
    }

    private void BuildDatabase()
    {
        //Skills = JsonConvert.DeserializeObject<List<Skill>>(Resources.Load<TextAsset>("JSON/Skills").ToString());
        Skills = JsonUtility.FromJson<List<Skill>>(Resources.Load<TextAsset>("JSON/Skills").ToString());
    }

    public Skill GetSkill(string skillSlug)
    {
        foreach (Skill skill in Skills)
        {
            if (skill.SkillSlug == skillSlug)
            {
                return skill;
            }
        }

        Debug.LogWarning("Couldn't find skill: " + skillSlug);

        return null;
    }
}
