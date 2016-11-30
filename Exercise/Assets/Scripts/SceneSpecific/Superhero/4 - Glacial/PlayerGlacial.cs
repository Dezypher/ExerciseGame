using UnityEngine;
using System.Collections;

public class PlayerGlacial : MonoBehaviour {

	public int hitPoints;

	public float leftHeld = 0;
	public float rightHeld = 0;

	public float leftHoldAmt;
	public float rightHoldAmt;

	public bool doingLeft = false;
	public bool doingRight = false;

	public UnityEngine.UI.Image pain;

	private Animator animator;
	private SpawnIceMonster spawnIceMonster;

	private bool enableLeft = true;
	private bool enableRight = true;

	private ArduinoConnector arduinoConnector;
	private GameLogic gameLogic;
	private ScoreRecorder scoreRecorder;

	void Awake() {
		animator = GetComponent<Animator> ();

		spawnIceMonster = GameObject.Find ("Spawner").GetComponent<SpawnIceMonster> ();
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		scoreRecorder = GameObject.Find ("ScoreRecorder").GetComponent<ScoreRecorder> ();
		arduinoConnector = GameObject.Find ("Arduino Connection").GetComponent<ArduinoConnector> ();
	}

	void Update() {
		if (!gameLogic.done && !gameLogic.pauseLogic) {
			if (spawnIceMonster.aliveLeft && enableLeft) {
				if ((Input.GetKey (KeyCode.A) || arduinoConnector.ExerciseAlive(gameLogic.exerciseIDs[0]))
					&& !spawnIceMonster.monsterLeft.GetComponent<IceMonster> ().notInPosition) {
					leftHeld += Time.deltaTime;

					doingLeft = true;
				} else {
					doingLeft = false;

					if (leftHeld > 0)
						leftHeld -= Time.deltaTime;
					else
						leftHeld = 0;
				}

				if (Input.GetKey (KeyCode.A) && !spawnIceMonster.monsterLeft.GetComponent<IceMonster> ().notInPosition) {
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

			if (spawnIceMonster.aliveRight && enableRight) {
				if ((Input.GetKey (KeyCode.D) || arduinoConnector.ExerciseAlive(gameLogic.exerciseIDs[0]))
					&& !spawnIceMonster.monsterRight.GetComponent<IceMonster> ().notInPosition) {
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
				StartCoroutine (Attack (0));
			}

			if (rightHeld >= rightHoldAmt) {
				StartCoroutine (Attack (1));
			}
		}
	}

	public IEnumerator Attack(int side) {
		if (side == 0) {
			leftHeld = 0;
			enableLeft = false;

			animator.SetTrigger ("HammerLeft");

			yield return new WaitForSeconds (0.5f);

			spawnIceMonster.Kill (0);
			enableLeft = true;
		} else {
			rightHeld = 0;
			enableRight = false;

			animator.SetTrigger ("HammerRight");

			yield return new WaitForSeconds (0.5f);

			spawnIceMonster.Kill (1);
			enableRight = true;
		}
		gameLogic.CustomScore (gameLogic.points, 100);

		gameLogic.points += 10;
		gameLogic.amtDone++;
	}

	public void Damage () {
		if (gameLogic.points > 0)
			gameLogic.points -= 10;

		Color color = pain.color;

		color.a = 0.5f;

		pain.color = color;
	}
}
