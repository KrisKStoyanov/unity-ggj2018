using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drone : MonoBehaviour
{
	[Header ("Audio")]
	private AudioSource droneAS;
	public AudioClip startMove;
	public AudioClip ongoigMove;
	public AudioClip rotateMove;

	public AudioClip[] fallingSound;

	public AudioClip selectedFall;

	public bool startEngine = false;
	public bool runningEngine = false;
	public bool rotatingEngine = false;

	public bool transition = false;

	public float fallSoundTime;
	public bool canPlayFall = true;

	public float timeUntilRE;

	public bool canRotate = true;
	public float rotateLength;

	[Header ("Setup")]
	public Transform camera;
	public CharacterController controller;

	[Header ("Movement")]
	public float walkSpeed = 8;
	public float runSpeed = 14;
	public float transitionSpeed = 1.0f;
	public float RotationSpeed = 20.0f;

	[Header ("Jump")]
	public float JumpVelocity = 10;
	public float fallMultiplayer = 2.5f;
	public float lowJumpMultiplier = 0.8f;

	[Header ("State")]
	public Vector3 Velocity = Vector3.zero;
	public float movementSpeed = 8.0f;

	private Vector3 lastEular;

	void Start()
	{
		droneAS = GetComponent<AudioSource> ();
		timeUntilRE = startMove.length;
		rotateLength = rotateMove.length;
	}

	private void Update ()
	{	
		Vector2 input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		Vector2 inputDir = input.normalized;

		if (controller.velocity.x != 0 && !startEngine && !runningEngine) {
			PlayDroneAudio (startMove);
			startEngine = true;
			StartCoroutine (WaitAndSwitchEngine ());
		}

		if (runningEngine && !startEngine && controller.velocity.x != 0 && !transition) {
			PlayDroneAudio(ongoigMove);
			transition = true;
		}

		if (runningEngine && controller.velocity.x != 0) {
			droneAS.loop = true;
		}

		if (controller.velocity.x == 0) {
			droneAS.Stop ();
			droneAS.loop = false;
			startEngine = false;
			runningEngine = false;
			transition = false;
			timeUntilRE = startMove.length;
			StopCoroutine (WaitAndSwitchEngine ());
		}

		float targetSpeed;

		if (Input.GetKey (KeyCode.LeftShift))
		{
			targetSpeed = runSpeed;
		}
		else
		{
			targetSpeed = walkSpeed;
		}

		movementSpeed = Mathf.Lerp (movementSpeed, targetSpeed, Time.deltaTime * transitionSpeed);

		Vector3 movement = new Vector3 (input.x, 0, input.y) * movementSpeed;

		if (!controller.isGrounded)
		{
			if (Velocity.y < 0)
			{
				Velocity += Physics.gravity * Time.deltaTime * fallMultiplayer;
			}
			else
			{
				if (Input.GetButton ("Jump"))
				{
					Velocity += Physics.gravity * Time.deltaTime * lowJumpMultiplier;
				}
				else
				{
					Velocity += Physics.gravity * Time.deltaTime * 1.0f;
				}
			}
		}
		else
		{
			if (Velocity.y < -2f) {

				selectedFall = fallingSound [Random.Range (0, fallingSound.Length)];
				droneAS.PlayOneShot (selectedFall);

			}

			Velocity.y = 0.0f;

			if (Input.GetButtonDown ("Jump"))
			{
				selectedFall = fallingSound [Random.Range (0, fallingSound.Length)];
				droneAS.PlayOneShot (selectedFall);

				Velocity = new Vector3 (Velocity.x, JumpVelocity, Velocity.z);
			}
		}

		Quaternion rotator = Quaternion.Euler (0, camera.transform.eulerAngles.y, 0);
		movement = rotator * movement;

		Quaternion inputRotator = Quaternion.Euler (0, camera.transform.eulerAngles.y, 0);
		Vector3 inputRotated = rotator * input;

		Vector3 noJump = new Vector3 (movement.x, 0, movement.z);

		if (noJump.magnitude > 0.5f) {

			noJump.Normalize ();
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (noJump),
				Time.deltaTime * RotationSpeed);
		}

		movement += Velocity;
		Velocity = new Vector3 (Velocity.x * 0.9f, Velocity.y, Velocity.z * 0.9f);

		float rotationPower = Mathf.Abs (Vector3.Distance (lastEular.normalized, transform.eulerAngles));

		/*if (rotationPower * Time.deltaTime > 10) {
			StartCoroutine (WaitBeforePlayingRotate ());
		}*/

		controller.Move (movement * Time.deltaTime);

		lastEular = transform.eulerAngles;
	}

	void PlayDroneAudio (AudioClip clipToPlay){
		droneAS.Stop ();
		droneAS.clip = clipToPlay;
		droneAS.Play ();
	}
	void StopDroneAudio (AudioClip clipToStop){
		droneAS.Stop ();
	}
		
	private IEnumerator WaitAndSwitchEngine() {
		while (true) {

			timeUntilRE -= Time.deltaTime;
			if (timeUntilRE <= 0) {
				startEngine = false;
				runningEngine = true;
				timeUntilRE = startMove.length;
				StopCoroutine ("WaitBeforePlayingFall");
			}

			yield return null;
		}
	}

	private IEnumerator WaitBeforePlayingFall() {
		if (canPlayFall) {
			selectedFall = fallingSound [Random.Range (0, fallingSound.Length)];
			droneAS.PlayOneShot (selectedFall);
			canPlayFall = false;
			fallSoundTime = selectedFall.length;
			yield return new WaitForSeconds (fallSoundTime);
			canPlayFall = true;
		}
	}

	/*private IEnumerator WaitBeforePlayingRotate(){
		if (canRotate) {
			droneAS.PlayOneShot (rotateMove);
			canRotate = false;
			rotateLength = rotateMove.length;
			yield return new WaitForSeconds (rotateLength);
			canRotate = true;
		}
	}*/
		
}