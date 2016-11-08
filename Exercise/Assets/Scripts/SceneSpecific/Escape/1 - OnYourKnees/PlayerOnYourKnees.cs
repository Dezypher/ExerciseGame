using UnityEngine;
using System.Collections;

public class PlayerOnYourKnees : MonoBehaviour {

	public bool isKneeled = false;
	public float kneelTime;
	public Animator animator;

	private GameLogic gameLogic;
	private ArduinoConnector arduinoConnector;
	private bool hasToReset = false;
	private bool isDoing = false;

	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic>();
		arduinoConnector = GameObject.Find ("Arduino Connection").GetComponent<ArduinoConnector> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameLogic.started && !gameLogic.done) {
			if (Input.GetButton ("DebugWin") && !isKneeled) {
				isDoing = true;
				gameLogic.timeHeld += Time.deltaTime;

				if (gameLogic.timeHeld >= gameLogic.holdTime) {
					gameLogic.timeHeld = 0;
					StartCoroutine (Kneel ());
				}
			} else if (arduinoConnector.exercises.Length > 0 && arduinoConnector.exercises [0].timeAlive > 0 && !isKneeled) {
				if (!hasToReset) {
					isDoing = true;
					gameLogic.timeHeld += Time.deltaTime;

					if (gameLogic.timeHeld >= gameLogic.holdTime) {
						hasToReset = true;
						gameLogic.timeHeld = 0;
						StartCoroutine (Kneel ());
					}
				}
			} else {
				isDoing = false;

				if (gameLogic.timeHeld > 0) {
					gameLogic.timeHeld -= Time.deltaTime;

					if (gameLogic.timeHeld <= 0) {
						gameLogic.timeHeld = 0;
					}
				}
			}

			if (hasToReset && arduinoConnector.exercises [0].timeAlive <= 0) {
				hasToReset = false;
			}
		}

		if (isDoing) {
			gameLogic.isDoingExercise = true;
		} else {
			gameLogic.isDoingExercise = false;
		}
	}

	public IEnumerator Kneel () { 
		isKneeled = true;
		animator.SetTrigger ("Kneel");

		yield return new WaitForSeconds (kneelTime);

		animator.SetTrigger ("Stand");
		isKneeled = false;
	}
}