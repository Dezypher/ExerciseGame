using UnityEngine;
using System.Collections;

public class PlayerRopeSwing : MonoBehaviour {

	public Vector3 target;

	public int amtSwung = 0;

	public float holdTime;
	public float timeHeld;

	public GameLogic gameLogic;
	public RopeSpawner ropeSpawner;
	public Animator animator;

	public bool swinging = false;

	public float speed = 1;
	public float speedModifier = 1;

	void Start () {
		//target = ropeSpawner.spawnedRopes [0].transform.FindChild ("Rope").transform.FindChild ("PlayerHold").position;
		ropeSpawner = GameObject.Find ("RopeSpawner").GetComponent<RopeSpawner> ();
		animator = GetComponent<Animator> ();	
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
	}

	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, target, Time.deltaTime * (speed * speedModifier));

		if (gameLogic != null) {
			if (gameLogic.started && !gameLogic.done) {
				GameLoop ();
			}
		} else
			GameLoop ();
	}


	void GameLoop() {
		if (Input.GetKey (KeyCode.Space) && !swinging) {
			if (gameLogic != null)
				gameLogic.isDoingExercise = true;

			timeHeld += Time.deltaTime;

			if (timeHeld >= holdTime) {
				timeHeld = 0;

				if (gameLogic != null) {
					gameLogic.amtDone++;
					gameLogic.points += 10;
				}
				StartCoroutine (Swing ());
			}
		} else {
			if (gameLogic != null)
				gameLogic.isDoingExercise = false;
		}
	}


	public IEnumerator Swing () {
		animator.SetTrigger ("Swing");
		ropeSpawner.spawnedRopes [amtSwung].GetComponent<Animator> ().SetTrigger ("Swing");
		target = new Vector3 (
			transform.position.x - 2f,
			transform.position.y + 0.5f,
			transform.position.z
		);

		speedModifier = 0.35f;

		amtSwung++;
		swinging = true;

		yield return new WaitForSeconds(0.5f);

		speedModifier = 1.5f;

		target = ropeSpawner.spawnedRopes [amtSwung].transform.FindChild ("Rope").transform.FindChild ("PlayerHold").position;
		yield return new WaitForSeconds (0.25f);

		speedModifier = 1;

		yield return new WaitForSeconds (0.25f);
		swinging = false;
	}
}
