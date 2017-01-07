using UnityEngine;
using System.Collections;

public class PlayerAnimPinata : MonoBehaviour {

	private GameLogic gameLogic;
	private bool wait = false;
	private Animator animator;

	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (!wait && gameLogic.done) {
			wait = true;
			animator.SetTrigger ("Swing");
		} 

		/*
		else if (wait == true && !gameLogic.hasToReset) {
			wait = false;
		}
		*/
	}
}