using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boar : MonoBehaviour {

	private Animator anim;
	private NavMeshAgent navAgent;

	public float currentHealth;
	public float maxHealth = 2f;
	public GameObject Hero;
	private bool isDead;
	Vector3 originalPosition;

	public GameObject healthBar;
	public GameObject healthBarPrefab;
	public Transform healthBarTransform;

	public CharacterMovement cm;

	void Start () {

		anim = GetComponent<Animator> ();
		navAgent = GetComponent<NavMeshAgent> ();
		currentHealth = maxHealth;

		healthBar = Instantiate (healthBarPrefab, healthBarTransform.transform) as GameObject;
		healthBar.transform.GetComponentInChildren<Slider> ().value = (currentHealth / maxHealth);

		originalPosition = transform.position;
	}

	void FixedUpdate () {

		if (!isDead) {
			healthBar.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y+2f, gameObject.transform.position.z);
			healthBar.transform.rotation = Quaternion.LookRotation (Camera.main.transform.forward, Camera.main.transform.up);
			transform.LookAt (Hero.transform);
		}

		if (CharacterMovement.isDead) {
			CancelInvoke ();
		}
	}

	public void TakeDamage (){
		if (!isDead) {
			anim.SetTrigger ("Take_Damage");
			currentHealth--;
			healthBar.GetComponentInChildren<Slider> ().value = (currentHealth / maxHealth);


			if (currentHealth <= 0) {
				Die ();
			}
		}

	}

	void ChaseHero(){
		navAgent.SetDestination (Hero.transform.position);
		anim.SetTrigger ("Moving");
		navAgent.speed = 1.0f;

		if (navAgent.remainingDistance <= navAgent.stoppingDistance) {

			if (!IsInvoking() && !CharacterMovement.isDead) {	
				InvokeRepeating ("Attack", 1f, 2f);
			}
		} else {
			CancelInvoke ();
		}
	}

	void Attack(){
		if (!isDead) {
			anim.SetTrigger ("Basic_Attack");
			cm.TakeDamage (2);
		}
	}

	void Die (){
		anim.SetTrigger ("Dead");
		isDead = true;
		//Debug.Log ("Boar is dead. isDead = "+ isDead);
		CancelInvoke ("Attack");
		gameObject.GetComponent<Collider> ().enabled = false;

		Invoke ("EndGame", 3f);

	}

	void EndGame(){
		SceneManager.LoadScene (2);
	}

	void OnTriggerStay (Collider col){
		if (col.name == "Hero" && !isDead) {
			//Debug.Log ("Boar: Enemy in sight!");

			ChaseHero ();
		}

	}

	void OnTriggerExit (Collider col){
		if (col.name == "Hero" && !isDead) {
			//Debug.Log ("Boar: Well, it's time to go home");

			StopChaseHero ();
		}
	}

	//Boar returns to its original position
	void StopChaseHero(){
		navAgent.SetDestination (originalPosition);
		navAgent.speed = 0.8f;
	}

}
