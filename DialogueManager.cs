using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public static DialogueManager Instance { get; set; }


	public List<string> dialogueLines = new List<string>();

	[HideInInspector]
	public int index;
	public GameObject dialoguePanel;
	[HideInInspector]
	public Text objName, lines;
	Button continueButton;
	public string npcName;
	public Image image;

	public AudioSource MetalTingSFX;


	void Awake(){
		continueButton = dialoguePanel.transform.Find ("Continue").GetComponent<Button>();
		objName = dialoguePanel.transform.Find ("Name").GetChild (0).GetComponent<Text>();
		lines = dialoguePanel.transform.Find ("Lines").GetComponent<Text>();
		image = dialoguePanel.transform.Find ("Image").GetComponent<Image> ();
		continueButton.onClick.AddListener (delegate{ ContinueDialogue (); });
	
		dialoguePanel.SetActive (false);

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}
	}

	void Update(){
		if ((Input.GetKeyDown(KeyCode.E) && dialoguePanel.activeInHierarchy) || (Input.GetKeyDown(KeyCode.Space) && dialoguePanel.activeInHierarchy) )
			ContinueDialogue ();
	}

	public void AddNewDialogue(string[] lines, string npcName, Sprite sprite){
		this.index = 0;
		image.sprite = sprite;
		dialogueLines = new List<string> ();

		for (int i = 0; i < lines.Length; i++) {
			dialogueLines.Add (lines [i]);
			//Debug.Log (dialogueLines[i]);
		}

		this.npcName = npcName;
		//Debug.Log ("index is: " + index);
		CreateDialogue ();
	}

	void CreateDialogue (){
		lines.text = dialogueLines[index];
		objName.text = npcName;
		image.enabled = true;
		dialoguePanel.SetActive (true);

	}

	void ContinueDialogue(){
		if (index < dialogueLines.Count - 1) {
			index++;
			MetalTingSFX.Play ();
			//Debug.Log ("Index is: " + index);
			lines.text = dialogueLines [index];

		} else {
			EndDialogue ();
			MetalTingSFX.Play ();

		}
	}

	public void CloseDialogue(){
		dialoguePanel.SetActive (false);
	}

	public bool EndDialogue(){
		dialoguePanel.SetActive (false);
		return true;
	}
}
