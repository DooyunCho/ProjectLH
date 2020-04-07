using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SkillController : MonoBehaviour {
    public static SkillController Instance { get; set; }
    public Transform ProjectileSpawn { get; set; }
    public Transform RangeObject { get; set; }
    public Transform RangeObject2D { get; set; }
    List<Skill> Skills = new List<Skill>();
    Fireball fireball;
    Skill printSkill;
    NavMeshAgent playerAgent;
    public RectTransform CastingUI;
    Image CastingBar;
    Text CastingTime;
    Image SkillImage;
    Text SkillName;

    private Coroutine spellRoutine;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        ProjectileSpawn = transform.Find("ProjectileSpawn");
        RangeObject = ProjectileSpawn.Find("RangeObject");
        //RangeObject2D = ProjectileSpawn.Find("RangeObject2D");
        fireball = Resources.Load<Fireball>("Weapons/Projectiles/Fireball");
        playerAgent = GetComponent<NavMeshAgent>();

        GiveSkill("fireball");
        GiveSkill("energybolt");
        //GiveSkill("Spear");
        //GiveSkill("PowerShot");

        RangeObject.gameObject.SetActive(false);
        CastingUI.gameObject.SetActive(false);

        CastingTime = CastingUI.Find("Time").GetComponent<Text>();
        CastingBar = CastingUI.Find("CastingBar").GetComponent<Image>();
        SkillImage = CastingUI.Find("SkillIcon").GetComponent<Image>();
        SkillName = CastingUI.Find("SkillName").GetComponent<Text>();
    }

    void Update()
    {
        if (printSkill != null)
        {
            playerAgent.angularSpeed = 0;
            RangeObject.gameObject.SetActive(true);
            Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactionInfo;

            if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                playerAgent.updateRotation = true;
                float distance = Vector3.Distance(interactionInfo.point, ProjectileSpawn.transform.position);
                distance = (distance > printSkill.SkillDistance) ? printSkill.SkillDistance : distance;
                ProjectileSpawn.transform.localScale = new Vector3(ProjectileSpawn.transform.localScale.x, ProjectileSpawn.transform.localScale.y, distance*5f);
            }
        }
        else
        {
            RangeObject.gameObject.SetActive(false);
        }
    }

    public void GiveSkill(string skillSlug)
    {
        Skill skill = SkillDatabase.Instance.GetSkill(skillSlug);
        Skills.Add(skill);
        Debug.Log(Skills.Count + " skill added: " + skill.SkillSlug);
        UIEventHandler.SkillAdded(skill);
    }

    public Skill GetSkill(string skillSlug)
    {
        for (int i = 0; i < Skills.Count; i++)
        {
            if (Skills[i].SkillSlug == skillSlug)
            {
                return Skills[i];
            }
        }

        return Skills[0];
    }

    /*public IEnumerator Fire(Skill skill, Vector3 point)
    {
        Object o = null;
        switch(skill.SkillSlug)
        {
            default:
            case "fireball":
                o = fireball;
                break;
        }

        StartCasting(skill);
        this.printSkill = null;

        yield return new WaitForSeconds(skill.Stats[1].BaseValue);

        Fireball fireballInstance = Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Damage = skill.Stats[0].BaseValue;
        fireballInstance.Range = skill.SkillDistance;
        fireballInstance.Direction = ProjectileSpawn.forward;

        Ray interactionRay = Camera.main.ScreenPointToRay(point);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity, LayerMask.NameToLayer("Unclickable")))
        {
            playerAgent.updateRotation = true;
            GameObject target = interactionInfo.collider.gameObject;
            
            if (target.tag == "Enemy")
            {
                fireballInstance.Target = target;
            }
        }
        StopCasting();
    }*/

    public IEnumerator Fire(Skill skill, GameObject target)
    {
        Debug.Log(skill.SkillName);
        StartCasting(skill);
        this.printSkill = null;

        yield return new WaitForSeconds(skill.Stats[1].BaseValue);

        //Fireball fireballInstance = Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        SkillInterface skillInstance = Instantiate(Resources.Load<SkillInterface>("Weapons/Projectiles/" + skill.SkillName), ProjectileSpawn.position, ProjectileSpawn.rotation);

        skillInstance.Damage = skill.Stats[0].BaseValue;
        skillInstance.Range = skill.SkillDistance;
        skillInstance.Direction = ProjectileSpawn.forward;
        skillInstance.Target = target;

        StopCasting();
    }

    public void PrintRange(Skill skill)
    {
        this.printSkill = skill;
    }

    public void StartCasting(Skill skill)
    {
        CastingBar.fillAmount = 0;
        CastingUI.gameObject.SetActive(true);

        CastingTime.text = "0.0";
        SkillName.text = skill.SkillName;
        SkillImage.sprite = Resources.Load<Sprite>("UI/Icons/Skills/" + skill.SkillSlug);

        switch (skill.ElementType)
        {
            case Skill.ElementTypes.Fire:
                CastingBar.color = Color.red;
                break;
            case Skill.ElementTypes.Water:
                CastingBar.color = Color.blue;
                break;
            case Skill.ElementTypes.Earth:
                CastingBar.color = Color.green;
                break;
            case Skill.ElementTypes.Wind:
                CastingBar.color = Color.cyan;
                break;
            case Skill.ElementTypes.Thunder:
                CastingBar.color = Color.yellow;
                break;
        }
        
        spellRoutine = StartCoroutine(Progress(skill.Stats[1].BaseValue));
    }

    private IEnumerator Progress(float time)
    {
        float timePassed = Time.deltaTime;
        float rate = 1.0f / time;
        float progress = 0.0f;

        while(progress <= 1.0)
        {
            CastingBar.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += (rate * Time.deltaTime);

            timePassed += Time.deltaTime;

            CastingTime.text = (time - timePassed).ToString("F2");

            if (time - timePassed < 0)
            {
                CastingTime.text = "0.00";
            }

            yield return null;
        }
    }

    public void StopCasting()
    {
        if (spellRoutine != null)
        {
            Debug.Log("StopCoroutine");
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }

        CastingUI.gameObject.SetActive(false);
    }
}
