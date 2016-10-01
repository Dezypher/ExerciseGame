using UnityEngine;
using System.Collections;

public class PlayerControllerTire : MonoBehaviour {
	
	public float leftHeld = 0;
	public float rightHeld = 0;

	public float leftHoldAmt;
	public float rightHoldAmt;

	public bool doingLeft = false;
	public bool doingRight = false;

	public bool inPosition = true;
	public Vector3 targetTire;

	public float speed;

	private Animator animator;
	private TireGenerator tireGenerator;

	private bool enableLeft = true;
	private bool enableRight = true;

	private GameLogic gameLogic;
	private ScoreRecorder scoreRecorder;

	void Awake() {
		animator = GetComponent<Animator> ();

		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		tireGenerator = GameObject.Find ("TireGenerator").GetComponent<TireGenerator>();
		scoreRecorder = GameObject.Find ("ScoreRecorder").GetComponent<ScoreRecorder> ();
	}

	void Update() {
		if (!gameLogic.done && !gameLogic.pauseLogic) {
			if (tireGenerator.positions[tireGenerator.currTire + 1] == 1 && enableLeft) {
				if (Input.GetKey (KeyCode.A)) {
					leftHeld += Time.deltaTime;

					doingLeft = true;
				} else {
					doingLeft = false;

					if (leftHeld > 0)
						leftHeld -= Time.deltaTime;
					else
						leftHeld = 0;
				}
			}

			if (tireGenerator.positions[tireGenerator.currTire + 1] == 0 && enableRight) {
				if (Input.GetKey (KeyCode.D)) {
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

			if (leftHeld >= leftHoldAmt) {
				Hop ();
				leftHeld = 0;
			}

			if (rightHeld >= rightHoldAmt) {
				Hop ();
				rightHeld = 0;
			}
		}

		if (!inPosition) {
			transform.position = Vector3.MoveTowards (transform.position, targetTire, Time.deltaTime * speed);

			if (Vector3.Distance (transform.position, targetTire) < 0.1) {
				inPosition = true;
			}
		}
	}

	public void Hop() {
		animator.SetTrigger ("Hop");

		targetTire = tireGenerator.tires [tireGenerator.currTire + 1].transform.GetChild(0).position;

		targetTire.y -= 0.8f;
		targetTire.x += 0.25f;

		tireGenerator.currTire++;
		inPosition = false;

		gameLogic.points += 10;
		gameLogic.amtDone++;

		if (gameLogic.amtDone == gameLogic.doAmt) {
			scoreRecorder.AddScore (gameLogic.points, 100, gameLogic.elapsedTime);
			gameLogic.CustomScore (gameLogic.points, 100);
		}
	}
}
