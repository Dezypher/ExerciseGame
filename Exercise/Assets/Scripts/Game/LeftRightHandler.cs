using UnityEngine;
using System.Collections;

public class LeftRightHandler : MonoBehaviour {

	public float leftHeld = 0;
	public float rightHeld = 0;

	public float leftHoldAmt;
	public float rightHoldAmt;

	public bool doingLeft = false;
	public bool doingRight = false;

	private bool enableLeft = true;
	private bool enableRight = true;

	private GameLogic gameLogic;
	private ScoreRecorder scoreRecorder;

	void Awake() {

		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		scoreRecorder = GameObject.Find ("ScoreRecorder").GetComponent<ScoreRecorder> ();
	}

	void Update() {
		if (!gameLogic.done && !gameLogic.pauseLogic) {
			if (Input.GetKey (KeyCode.A) && enableLeft) {
				leftHeld += Time.deltaTime;

				doingLeft = true;
			} else {
				doingLeft = false;

				if (leftHeld > 0)
						leftHeld -= Time.deltaTime;
				else
					leftHeld = 0;
			}

			if (Input.GetKey (KeyCode.D) && enableRight) {
				rightHeld += Time.deltaTime;

				doingRight = true;
			} else {
				doingRight = false;

				if (rightHeld > 0)
					rightHeld -= Time.deltaTime;
				else
					rightHeld = 0;
			}
		}
	}
}
