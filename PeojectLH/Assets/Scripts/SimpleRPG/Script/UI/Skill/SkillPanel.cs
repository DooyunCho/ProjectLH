using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    public RectTransform PanelUI;
    public RectTransform scrollViewContent;
    SkillUIItem itemContainer { get; set; }
    bool menuIsActive { get; set; }

    // Use this for initialization
    void Start ()
    {
        itemContainer = Resources.Load<SkillUIItem>("UI/SkillPanel/SkillContainer");
        UIEventHandler.OnSkillAdded += SkillAdded;
        PanelUI.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	/*void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P) || (Input.GetKeyDown(KeyCode.Escape) && menuIsActive))
        {
            menuIsActive = !menuIsActive;
            PanelUI.gameObject.SetActive(menuIsActive);
        }
    }*/

    void SkillAdded(Skill skill)
    {
        Debug.Log("Skill added " + skill.SkillSlug);

        SkillUIItem emptyItem = Instantiate(itemContainer);
        emptyItem.SetSkill(skill);
        emptyItem.transform.SetParent(scrollViewContent);
    }
}
