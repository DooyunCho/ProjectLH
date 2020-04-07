using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable{
    public string[] dialog;
    public string name;

    public override void Interact()
    {
        DialogSystem.Instance.AddNewDialog(dialog, name);
        Debug.Log("Interacting with NPC.");
    }
}
