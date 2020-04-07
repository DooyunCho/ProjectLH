using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    private bool isActive { get; set; }
    public RectTransform CharacterPanelUI;

    // Use this for initialization
    void Start ()
    {
        isActive = false;
        CharacterPanelUI.gameObject.SetActive(isActive);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.C))// || (Input.GetKeyDown(KeyCode.Escape) && isActive))
        {
            isActive = !isActive;
            CharacterPanelUI.gameObject.SetActive(isActive);
        }
    }
}
