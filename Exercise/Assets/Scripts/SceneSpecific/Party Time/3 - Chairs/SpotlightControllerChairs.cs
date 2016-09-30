using UnityEngine;
using System.Collections;

public class SpotlightControllerChairs : MonoBehaviour {

	public bool lightsOff = true;

	public float currTime = 0;

	public float stayMin;
	public float stayMax;

	public float pauseMin;
	public float pauseMax;

	public bool left = false;
	public bool right = false;

	public GameObject spotLightLeft;
	public GameObject spotLightRight;

	public GameLogic gameLogic;

	public int lastLight = 0;

	public Animator animator;

	void Start() {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();

		currTime = Random.Range (pauseMin, pauseMax);
	}

	void Update () {
		if (!gameLogic.done && !gameLogic.pauseLogic) {
			currTime -= Time.deltaTime;

			spotLightLeft.SetActive (left);
			spotLightRight.SetActive (right);

			if (currTime <= 0) {
				if (lightsOff) {
					currTime = Random.Range (stayMin, stayMax);
					lightsOff = false;

					switch (lastLight) {
					case 0:
						lastLight = 1;
						right = true;
						left = false;
						break;
					case 1: 
						lastLight = 0;
						right = false;
						left = true;
						break;
					}
				} else {
					currTime = Random.Range (pauseMin, pauseMax);
					left = false;
					right = false;
					lightsOff = true;
				}
			}

			if (Input.GetKey (KeyCode.A)) {
				animator.SetBool ("Right", true);

				if(left) {
					gameLogic.overrideInput = true;
					gameLogic.isDoingExercise = true;
				} else { 
					gameLogic.overrideInput = false;
					gameLogic.isDoingExercise = false;
				}
			} else 
				animator.SetBool ("Right", false);
			
			if (Input.GetKey (KeyCode.D)) {
				animator.SetBool ("Left", true);

				if(right) {
					gameLogic.overrideInput = true;
					gameLogic.isDoingExercise = true;
				} else { 
					gameLogic.overrideInput = false;
					gameLogic.isDoingExercise = false;
				}
			} else 
				animator.SetBool ("Left", false);
		}
	}
}
