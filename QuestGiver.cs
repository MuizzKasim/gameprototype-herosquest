using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : Interactive {

	public string[] lines;
	public string objName;
	public Sprite sprite;

	public bool questThree;
	public QuestManager qm;

	void Start () {
		questThree = false;

		objName = "Red Slime";
		InitQuestOne ();
	}
	

	void Update () {

	}

	void OnTriggerEnter (Collider col){
		Debug.Log ("We're in");
		if (col.name == "Hero") {
			Interact ();
		}

		if (questThree == true && col.name == "Hero") {
			Interact ();
			qm.CompleteQuestThree ();
		}
			
	}
		
	void OnTriggerExit (Collider col){
		Debug.Log ("We're out");
		if (col.name == "Hero") {
			NotInteract ();
		}
	}

	void InitQuestOne(){
		
		lines = new string[21];

		lines [0] = "Hey there buddy";
		lines [1] = "This world's a dangerous place you know?";
		lines [2] = "I would advice you press W,A,S,D to move around";
		lines [3] = "and hold Shift while doing it to sprint";
		lines [4] = "Watch out for your depleting stamina however";
		lines [5] = "Because having no stamina forces you to stop for a while";
		lines [6] = "Which can mean the end for you.";
		lines [7] = "Anyhow,";
		lines [8] = "If you want venture further out there";
		lines [9] = "You will have to go through the big gate to the north";
		lines [10] = "But you won't ever go through the gate";
		lines [11] = "Unless you've got with yourself";
		lines [12] = "The three special keys";
		lines [13] = "The first one can be found east of here";
		lines [14] = "Go there and start off your adventure buddy!";
		lines [15] = "Oh, but before you go on ahead";
		lines [16] = "I would advice you to go and get yourself a sword";
		lines [17] = "Hm? Where can you get a sword?";
		lines [18] = "Go to the south from here, and you will find a sword at the end";
		lines [19] = "It's my family heirloom, but don't feel bad about taking it";
		lines [20] = "We're just a bunch of slimes, a sword won't do us any good"; 

	}

	public void InitSwordUsage(){

		lines = new string [5];

		lines [0] = "Looks like you have a sword with you now";
		lines [1] = "You can click the Left Mouse Button to swing it";
		lines [2] = "Though, you probably already knew about that";
		lines [3] = "Just one request I have for you";
		lines [4] = "Please don't point it at me";


	}

	public void InitQuestTwo(){

		lines = new string [6];

		lines [0] = "You've got the first key buddy? Good";
		lines [1] = "Now, you can go to the west from here to get the next key";
		lines [2] = "If you haven't got yourself a sword already";
		lines [3] = "You should go to the south from here to get one";
		lines [4] = "Because the road ahead is filled with evil monsters";
		lines [5] = "And you will be needing that sword to protect yourself";

	}

	public void InitQuestThree(){

		lines = new string [13];

		lines [0] = "So, you've already got the second key";
		lines [1] = "Now, you would only need one more key to pass through the northern gate";
		lines [2] = "I hate to break it to you";
		lines [3] = "But I don't know where the third key is located";
		lines [4] = ". . .";
		lines [5] = "I'm just kidding";
		lines [6] = "Ofcourse, I know where it is";
		lines [7] = "The third key is inside me!";
		lines [8] = "I'm a slime after all! I can't resist engulfing important objects with me";
		lines [9] = ". . .";
		lines [10] = "Eherm, anyhow,";
		lines [11] = "Here you go, you can have it";
		lines [12] = "Don't worry, I keep myself clean at all times";

		questThree = true;
	}

	public override void Interact ()
	{
		DialogueManager.Instance.AddNewDialogue(lines, objName, sprite);
	}

	public override void NotInteract ()
	{
		DialogueManager.Instance.CloseDialogue ();
	}
}
