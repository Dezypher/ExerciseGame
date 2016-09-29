using UnityEngine;
using System.Collections;

public class PlayerAnimNinja : MonoBehaviour {

	private GameLogic gameLogic;
	private Animator animator;
	private bool wait = false;
	private UnityEngine.UI.Image fade;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		fade = GameObject.Find ("Fade").GetComponent<UnityEngine.UI.Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameLogic.done && gameLogic.started && !wait) {
			if (fade.color.a == 1) {
				wait = true;
				animator.SetTrigger ("NextPhase");
			}
		}

		if (!gameLogic.started) {
			wait = false;
		}
	}
}