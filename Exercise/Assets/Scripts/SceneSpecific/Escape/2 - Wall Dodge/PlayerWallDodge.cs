using UnityEngine;
using System.Collections;

public class PlayerWallDodge : MonoBehaviour {

	public const int STATE_IDLE = 0;
	public const int STATE_DODGE_LEFT = 1;
	public const int STATE_DODGE_RIGHT = 2;

	public int playerState = STATE_IDLE;
	public float dodgeTime;
	public Animator animator;

	public float holdTimeLeft;
	public float holdTimeRight;
	public float timeHeldLeft = 0;
	public float timeHeldRight = 0;

	public GameLogic gameLogic;
	private ArduinoConnector arduinoConnector;

	private bool hasToReset = false;
	private bool dodging = false;
	private bool isDoingLeft = false;
	private bool isDoingRight = false;

	// Use this for initialization
	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic>();
		arduinoConnector = GameObject.Find ("Arduino Connection").GetComponent<ArduinoConnector> ();
		animator = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameLogic.started && !gameLogic.done) {
			if (Input.GetKey (KeyCode.A) && !dodging) {
				timeHeldLeft += Time.deltaTime;

				if (timeHeldLeft >= holdTimeLeft) {
					StartCoroutine (Dodge (STATE_DODGE_LEFT));
					timeHeldLeft = 0;
				}
			} else {
				if (timeHeldLeft > 0) {
					timeHeldLeft -= Time.deltaTime;

					if (timeHeldLeft <= 0) {
						timeHeldLeft = 0;
					}
				}
			}
		}

		if (gameLogic.started && !gameLogic.done) {
			if (Input.GetKey (KeyCode.D) && !dodging) {
				timeHeldRight += Time.deltaTime;

				if (timeHeldRight >= holdTimeRight) {
					StartCoroutine (Dodge (STATE_DODGE_RIGHT));
					timeHeldRight = 0;
				}
			} else {
				if (timeHeldRight > 0) {
					timeHeldRight -= Time.deltaTime;

					if (timeHeldRight <= 0) {
						timeHeldRight = 0;
					}
				}
			}
		}
	}

	public IEnumerator Dodge (int state) {
		playerState = state;
		dodging = true;

		switch (state) {
		case STATE_DODGE_LEFT:
			animator.SetTrigger ("DodgeLeft");
			break;
		case STATE_DODGE_RIGHT:
			animator.SetTrigger("DodgeRight");
			break;
		}

		yield return new WaitForSeconds (dodgeTime);

		dodging = false;

		playerState = STATE_IDLE;
	}
}
