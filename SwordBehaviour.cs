using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour {

	private Animator anim;
	public GameObject sword;
	public bool isAttacking = false;

	public static bool activate = false;
	public AudioSource SwingSFX;

	void Start () {
		anim = GetComponent<Animator> ();
		sword.GetComponent<Collider>().enabled = false;

	}

	void Update(){

		if (activate == true) {
			ActivateSword ();
		}

		//If left mouse button is clicked, (and Hero is not dead!) causes the Hero to swing his sword.
		if (Input.GetMouseButtonDown (0) && CharacterMovement.isDead != true && sword.activeInHierarchy) {
			anim.SetTrigger ("Basic_Attack");
			SwingSFX.PlayDelayed (0.1f);
			StartCoroutine("HandleSwordCollider");
		}
			
	}

	IEnumerator HandleSwordCollider(){
		//Turns on sword collider, allowing Hero to deal damage to enemy.
		yield return new WaitForSeconds(0.4f);
		Debug.Log ("Part One");

		sword.GetComponent<Collider> ().enabled = true;
		isAttacking = true;

		//After .14 seconds (duration of animation), turn off sword collider

		yield return new WaitForSeconds (0.1f);
		Debug.Log ("Part Two");
		sword.GetComponent<Collider> ().enabled = false;
		isAttacking = false;
	}

	// Update is called once per frame
	void OnTriggerEnter (Collider col){
		if (col.gameObject.tag == "Enemy" && isAttacking ) {
			
			col.gameObject.GetComponentInParent<SlimeBehaviour> ().TakeDamage ();

		} 

		if (col.gameObject.tag == "Boss" && isAttacking) {
			
			col.gameObject.GetComponentInParent<Boar> ().TakeDamage ();
		}

	}

	void ActivateSword(){
		sword.SetActive (true);
		activate = false;
	}

	public static void EnableSword(){
		activate = true;
	}
}
