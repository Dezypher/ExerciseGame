using UnityEngine;
using System.Collections;

public class PlankAnimation : MonoBehaviour {

	private Animator animator;
	private GameLogic gameLogic;

	// Use this for initialization
	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {	
		if (gameLogic.isDoingExercise)
			animator.SetBool ("Planking", true);
		else 
			animator.SetBool ("Planking", false);
	}
}
