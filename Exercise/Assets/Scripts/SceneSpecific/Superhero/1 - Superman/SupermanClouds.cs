using UnityEngine;
using System.Collections;

public class SupermanClouds : MonoBehaviour {

	private ParticleSystem particleSystem;
	private GameLogic gameLogic;

	// Use this for initialization
	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		particleSystem = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {	
		ParticleSystem.EmissionModule emission = particleSystem.emission;

		if (gameLogic.isDoingExercise)
			emission.enabled = true;
		else 
			emission.enabled = false;
	}
}
