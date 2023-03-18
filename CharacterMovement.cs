using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

	public float moveSpeed = 1.5f;
	public float turnSmoothing = 15f;

	public float currentHp;
	public float maxHp = 10f;
	public float currentStamina;
	public float maxStamina = 15f;

	public bool canMove = true;
	public bool toRun = false;
	public bool isMoving = false;
	public bool isPaused = false;
	public bool canPause = true;

	public static bool isDead = false;
	public static bool isResting = false;

	public GameObject fadeIn;
	public GameObject fadeOut;
	public GameObject deathMsg;
	public GameObject quitBtn;
	public GameObject restartBtn;
	public Slider healthBar;
	public Slider staminaBar;

	//Music & Sounds
	public AudioSource deathSound;
	public AudioSource deathScream;
	public AudioSource Hurt1;
	public AudioSource Hurt2;
	public AudioSource ReplenishHealthSFX;
	public AudioSource WalkSFX;

	public AudioSource BGM;

	private Rigidbody rb;
	private Animator anim;

	void Awake () {
		Screen.SetResolution(1280, 720, true);

		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();

		deathMsg.SetActive (false);
		quitBtn.SetActive (false);
		restartBtn.SetActive (false);

		currentHp = maxHp;
		healthBar.value = currentHp / maxHp;
		currentStamina = maxStamina;
		staminaBar.value = currentStamina / maxStamina;

		BGM.Play ();
	}

	void Update(){
		
		if (currentStamina <= 0) {
			canMove = false; 
			toRun = false;
			Invoke ("SetCanMove", 2f);
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isMoving", false);
			CancelInvoke ("DepleteStamina");
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			Pause ();
		}
		}


	void FixedUpdate () {

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		//Moving (Walk)
		if (!isDead && canMove) {
			Move (h, v);

			//Running 
			if (Input.GetKeyDown (KeyCode.LeftShift) && !(currentStamina < 0) && isMoving) {
				toRun = true;
				CancelInvoke ("RestoreStamina");
				InvokeRepeating ("DepleteStamina", 0f, 0.025f);
			}

			if (Input.GetKeyUp (KeyCode.LeftShift)) {
				toRun = false;
				CancelInvoke ("DepleteStamina");
				InvokeRepeating ("RestoreStamina", 1f, 0.025f);
			}
		}

		if (isResting == true && !isDead) {
			StartCoroutine("Rest");
		}

	}
	
	void SetCanMove(){
		
		canMove = true;
		currentStamina += 0.0625f;
		staminaBar.value = currentStamina / maxStamina;
		
	}

	void DepleteStamina(){

		Debug.Log ("KeyDown");
		if (!(currentStamina < 0))
			currentStamina -= 0.0875f;
		
		staminaBar.value = currentStamina / maxStamina;

		
	}

	void RestoreStamina(){

		if (currentStamina < maxStamina)
			currentStamina += 0.0625f;
		
		else if (currentHp > maxStamina)
			currentStamina = maxStamina;

		staminaBar.value = currentStamina / maxStamina;
	}
		

	void Move (float horizontal, float vertical) {

		if (horizontal != 0f && !toRun || vertical != 0f && !toRun) {
			isMoving = true;
			transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			if(!WalkSFX.isPlaying)
				WalkSFX.PlayDelayed (0.2f);
			Turn (horizontal, vertical);

			anim.SetFloat ("SpeedBoost", 1.0f);
			anim.SetBool ("isMoving", true);
			anim.SetBool ("isIdle", false);


		} else if (horizontal != 0f && toRun || vertical != 0f && toRun && !(currentStamina < 0)) {
			isMoving = true;
			transform.Translate (Vector3.forward * (moveSpeed + 1f) * Time.deltaTime);
			if (!WalkSFX.isPlaying)
				WalkSFX.Play();
			Turn (horizontal, vertical);

			anim.SetFloat ("SpeedBoost", 2.0f);
			anim.SetBool ("isMoving", false);
			anim.SetBool ("isIdle", false);


		} else {
			isMoving = false;
			anim.SetBool ("isMoving", false);
			anim.SetBool ("isIdle", true);
		}
	}

	void Turn (float horizontal, float vertical) {

		Vector3 direction = new Vector3 (horizontal, 0, vertical);

		direction = Camera.main.transform.TransformDirection (direction);
		direction.y = 0f;

		Quaternion rotation = Quaternion.LookRotation (direction, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (rb.rotation, rotation, turnSmoothing * Time.deltaTime);

		rb.MoveRotation (newRotation);
	}

	public void TakeDamage(int damage){

		if (!isDead) {
			anim.SetTrigger ("Take_Damage");
			this.currentHp -= damage;
			int x = Random.Range (0, 2);

			if (x == 0) {
				Hurt1.Play ();
			} else {
				Hurt2.Play ();
			}

			healthBar.value = currentHp / maxHp;
		}
		//Debug.Log ("Hero hp is: " + hp);

		if (currentHp <= 0) {
			HeroDie ();
		}
	}

	void HeroDie(){
		isDead = true;
		anim.SetTrigger ("Dead");
		BGM.Stop ();
		deathScream.PlayDelayed (1f);

		Invoke ("DeathEffect", 2f);
		Invoke ("DeathButtons", 5f);
		//Debug.Log ("Hero died");
	}

	void DeathEffect(){
		deathMsg.SetActive (true);
		deathSound.Play ();
	}

	void DeathButtons(){
		quitBtn.SetActive (true);
		restartBtn.SetActive (true);
	}

	public static void SetRest(bool state){
		isResting = state;
	}

	IEnumerator Rest(){
		isResting = false;
		canMove = false;
		fadeIn.SetActive (true);

		if (currentHp == maxHp) {

			yield return new WaitForSeconds (1f);
			fadeIn.SetActive (false);
			fadeOut.SetActive (true);

			yield return new WaitForSeconds (1f);
			canMove = true;
			fadeOut.SetActive (false);
		}

		if (currentHp < maxHp) {
			InvokeRepeating ("ReplenishHealth", 1f, 1f);

			yield return new WaitUntil (() => currentHp == maxHp);
			fadeIn.SetActive (false);
			fadeOut.SetActive (true);

			yield return new WaitForSeconds (1f);
			canMove = true;
			fadeOut.SetActive (false);
		}
	}

	void ReplenishHealth (){
		if (currentHp < maxHp) {
			ReplenishHealthSFX.Play ();
			currentHp += 1.5f;

			if (currentHp > maxHp) {
				currentHp = maxHp;
			}

			healthBar.value = currentHp / maxHp;
			
		} else if (currentHp == maxHp) {
			
			CancelInvoke ("ReplenishHealth");
		}
	}

	void Pause(){
		if (canPause) {
			Time.timeScale = 0;
			isPaused = true;
			canPause = false;
		} else {
			Time.timeScale = 1;
			isPaused = false;
			canPause = true;
		}
	}



	/*//If player collides with an enemy, take damage
	void OnCollisionEnter (Collision other){	
		Debug.Log ("Hero entered collision");

		if (other.gameObject.tag == "Enemy" & !isDead & !damageRecovery){

			damageRecovery = true;
			StartCoroutine ("TakeDamage");


		}
	}

	IEnumerator TakeDamage (){
		this.hp--;
		Debug.Log ("Hero took damage");
		anim.SetBool ("isDamaged", true);


		if (hp == 0) {

			//If hp is 0, then player is dead
			anim.SetBool ("isDamaged", false);
			StartCoroutine ("PlayerDead");
		} else {
			
			yield return new WaitForSeconds (0.2f);
			anim.SetBool ("isDamaged", false);
			yield return new WaitForSeconds (0.8f);
			damageRecovery = false;
		}
	}
		

	IEnumerator PlayerDead(){

		isDead = true;
		anim.SetBool ("isMoving", false);
		anim.SetBool ("isDamaged", false);
		anim.SetBool ("isDead", false);
		anim.SetBool ("isDead", true);

		yield return new WaitForSeconds (0.5f);
		anim.SetBool ("isDead", false);

		yield return new WaitForSeconds (3f);
		Debug.Log ("Hero died");
	}*/
}
