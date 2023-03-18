using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SlimeBehaviour : MonoBehaviour {

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

	public AudioSource DamagedSFX;
	public AudioSource DeathSFX;
	public AudioSource MoveSFX;
	public AudioSource AttackSFX;

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
			healthBar.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y+4f, gameObject.transform.position.z);
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
			DamagedSFX.Play ();

			//Debug.Log ("Slime health: " + currentHealth);

			if (currentHealth <= 0) {
				Die ();
			}
		}

	}
		
	void ChaseHero(){
		navAgent.SetDestination (Hero.transform.position);
		anim.SetTrigger ("Moving");
		navAgent.speed = 1.0f;

		if (!MoveSFX.isPlaying) {
			MoveSFX.PlayDelayed (0.25f);
		}

		if (navAgent.remainingDistance <= navAgent.stoppingDistance) {

			if (MoveSFX.isPlaying) {
				MoveSFX.Stop ();
			}

			if (!IsInvoking () && !CharacterMovement.isDead) {	
				InvokeRepeating ("Attack", 1f, 2f);
			}
		} else {
			CancelInvoke ();
		}
	}

	void Attack(){
		if (!isDead) {
			anim.SetTrigger ("Basic_Attack");
			AttackSFX.Play ();
			cm.TakeDamage (1);
		}
	}

	void Die (){
		anim.SetTrigger ("Dead");
		isDead = true;
		//Debug.Log ("Slime is dead. isDead = "+ isDead);
		CancelInvoke ("Attack");
		gameObject.GetComponent<Collider> ().enabled = false;
		DeathSFX.Play ();

		Invoke ("RemoveBody", 5f);
	}

	void RemoveBody(){
		//Debug.Log ("This method was run");
		Destroy (gameObject);
		healthBar.SetActive (false);
	}

	void OnTriggerStay (Collider col){
		if (col.name == "Hero" && !isDead) {
			//Debug.Log ("Slime: Enemy in sight!");

			ChaseHero ();
		}

	}

	void OnTriggerExit (Collider col){
		if (col.name == "Hero" && !isDead) {
			//Debug.Log ("Slime: Well, it's time to go home");

			StopChaseHero ();
		}
	}

	//Slime returns to its original position
	void StopChaseHero(){
		navAgent.SetDestination (originalPosition);
		navAgent.speed = 0.8f;
	}
		
}
