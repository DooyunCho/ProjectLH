using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    public static AnimationController Instance;
    private Animator animator;

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

        animator = transform.Find("PlayerModel").GetComponent<Animator>();
    }

    public bool isTrigger(string triggerName)
    {
        return animator.GetBool(triggerName);
    }
    public void Rebind()
    {
        animator.Rebind();
    }

    public void setTrigger(string triggerName, bool value)
    {
        if (value)
        {
            animator.SetTrigger(triggerName);
        }
        else
        {
            //Debug.Log("reset trigger: " + triggerName);
            animator.ResetTrigger(triggerName);
        }
    }
}
