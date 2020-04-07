using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIItem : MonoBehaviour
{
    public Skill skill;
    public Image skillIcon;
    public Text skillName;

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
        skillName = this.transform.Find("Name").GetComponent<Text>();
        skillIcon = this.transform.Find("Icon").GetComponent<Image>();

        SetupSkillValues();
    }

    void SetupSkillValues()
    {
        this.skillName.text = skill.SkillName;
        this.skillIcon.sprite = Resources.Load<Sprite>("UI/Icons/Skills/" + skill.SkillSlug);

        //Debug.Log("SkillUIItem SkillSlug " + skill.SkillSlug);
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // 우클릭 중 마우스 트래킹
        {
            Debug.Log("GetMouseButton " + Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(1)) // 트래킹 해제
        {
            Debug.Log("GetMouseButtonUp " + Input.mousePosition);
        }
    }
}
