using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

	public bool questOne, questTwo, questThree;
	public QuestGiver qg;
	public GameObject sword, keyOne, keyTwo;

	void Start(){
		questOne = false;
		questTwo = false;
		questThree = false;
	}

	void Update (){

		if (sword.activeInHierarchy) {
			CompleteSwordAdvice ();
		}

		if (!keyOne.activeInHierarchy) {
			CompleteQuestOne ();
		}

		if (!keyTwo.activeInHierarchy) {
			CompleteQuestTwo ();
		}
			
	}

	void CompleteSwordAdvice(){
		qg.InitSwordUsage ();
	}

	public void CompleteQuestOne(){
		questOne = true;
		qg.InitQuestTwo ();
	}

	public void CompleteQuestTwo(){
		questTwo = true;
		qg.InitQuestThree ();
	}

	public void CompleteQuestThree(){
		questThree = true;
	}

	/*public static QuestManager Instance { get; set; }

	public bool[] questLog = new bool[3];

	public GameObject questPanel;

	[HideInInspector]
	public Text name, description, status;
	public int index;
	public bool completed;

	void Awake () {
		name = questPanel.transform.Find ("Name").GetComponent<Text>();
		description = questPanel.transform.Find ("Description").GetComponent<Text> ();
	}

	public void AddNewQuest (string name, string description, int questNo){

		this.description.text = description;
		this.name.text = name;

		questLog.SetValue (false, questNo - 1);
		this.status.text = "In Progress";

		questPanel.SetActive (true);
	}

	void CompleteQuest (int questNo){
		questLog.SetValue (true, questNo - 1);
		status.text = "Completed";
	}

	public bool TraverseAll(){

		if(questLog[0] && questLog[1] && questLog[2] == true)
				return true;
			else
				return false;
	}*/
}
