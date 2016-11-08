using UnityEngine;
using System.Collections;

public class PlayerOnYourKnees : MonoBehaviour {

	public bool isKneeled = false;
	public float kneelTime;

	public GameLogic gameLogic;
	private ArduinoConnector arduinoConnector;
	private bool hasToReset = false;

	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic>();
		arduinoConnector = GameObject.Find ("Arduino Connection").GetComponent<ArduinoConnector> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameLogic.started && !gameLogic.done) {
			if (Input.GetButton ("DebugWin") && !isKneeled) {
				Debug.Log ("Magic");

				gameLogic.timeHeld += Time.deltaTime;

				if (gameLogic.timeHeld >= gameLogic.holdTime) {
					gameLogic.timeHeld = 0;
					StartCoroutine (Kneel ());
				}
			} else if (arduinoConnector.exercises [0].timeAlive > 0 && !isKneeled) {
				if (!hasToReset) {
					gameLogic.timeHeld += Time.deltaTime;

					if (gameLogic.timeHeld >= gameLogic.holdTime) {
						hasToReset = true;
						gameLogic.timeHeld = 0;
						StartCoroutine (Kneel ());
					}
				}
			} else {
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

		if (Input.GetButton ("DebugWin") || arduinoConnector.exercises [0].timeAlive > 0) {
			gameLogic.isDoingExercise = true;
		} else {
			gameLogic.isDoingExercise = false;
		}
	}

	public IEnumerator Kneel () { 
		isKneeled = true;

		yield return new WaitForSeconds (kneelTime);

		isKneeled = false;
	}
}