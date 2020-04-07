using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {
    public static DialogSystem Instance { get; set; }
    public GameObject dialogPanel;
    public string npcName;
    public List<string> dialogLines = new List<string>();

    Button continueButton;
    Text dialogText, nameText;
    int dialogIndex;

	// Use this for initialization
	void Awake () {
        continueButton = dialogPanel.transform.Find("Continue").GetComponent<Button>();
        dialogText = dialogPanel.transform.Find("Text").GetComponent<Text>();
        nameText = dialogPanel.transform.Find("Name").GetChild(0).GetComponent<Text>();

        continueButton.onClick.AddListener(delegate { ContinueDialog(); });
        dialogPanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
	}

    public void AddNewDialog(string[] lines, string npcName)
    {
        dialogIndex = 0;
        dialogLines = new List<string>();

        foreach (string line in lines)
        {
            dialogLines.Add(line);
        }

        this.npcName = npcName;
        CreateDialog();
        Debug.Log(dialogLines.Count);
    }

    public void CreateDialog()
    {
        dialogText.text = dialogLines[dialogIndex];
        nameText.text = npcName;
        dialogPanel.SetActive(true);
    }

    public void ContinueDialog()
    {
        if (dialogIndex < dialogLines.Count-1)
        {
            dialogText.text = dialogLines[++dialogIndex];
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
}
