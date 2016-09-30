using UnityEngine;
using System.Collections;

public class PlayerAnimPop : MonoBehaviour {

	public GameObject balloonPrefab;

	public GameObject currBalloon;
	public GameObject balloonPopEffect;

	public Transform balloonSpawn;
	public Transform balloonTarget;

	private GameLogic gameLogic;
	private bool wait = false;
	private Animator animator;

	public bool balloonAtTarget = false;

	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameLogic.done) {
			if (wait == false && gameLogic.hasToReset) {
				StartCoroutine (PopBalloon ());
			} else if (wait == true && !gameLogic.hasToReset) {
				wait = false;
			}

			if (!balloonAtTarget) {
				currBalloon.transform.position = 
					Vector3.MoveTowards (currBalloon.transform.position, 
					balloonTarget.transform.position, 
					Time.deltaTime * 10);

				if (Vector3.Distance (currBalloon.transform.position, balloonTarget.transform.position) == 0.1)
					balloonAtTarget = true;
			}
		}
	}

	public IEnumerator PopBalloon() {
		wait = true;
		animator.SetTrigger ("Squat");

		yield return new WaitForSeconds(0.25f);

		//Destroy balloon make pop animation
		Destroy (currBalloon);
		currBalloon = (GameObject)Instantiate (balloonPrefab, balloonSpawn.transform.position, balloonPrefab.transform.rotation);

		Instantiate (balloonPopEffect, balloonTarget.transform.position, balloonPrefab.transform.rotation);
		balloonAtTarget = false;
	}
}
