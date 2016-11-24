
using UnityEngine;
using System.Collections;

public class PlayerClimb : MonoBehaviour {
	
	public Animator animator;
	public HoldSpawner holdSpawner;
	public GameLogic gameLogic;

	public int amountClimbed = 0;

	public Vector3 target;

	public bool climbing = false;

	public float speed;

	public float holdTimeLeft;
	public float timeHeldLeft;
	public float holdTimeRight;
	public float timeHeldRight;

	void Start () {
		holdSpawner = GameObject.Find ("HoldSpawner").GetComponent<HoldSpawner> ();
		animator = GetComponent<Animator> ();
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
	}

	void Update () {
		//if (gameLogic.started && !gameLogic.done) {

			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);

			if (Input.GetKey (KeyCode.A) && !climbing) {
				if (holdSpawner.sequence [amountClimbed] == HoldSpawner.LEFT) {
					if (timeHeldLeft < holdTimeLeft) {
						timeHeldLeft += Time.deltaTime;
					} else {
						timeHeldLeft = 0;
						animator.SetTrigger ("Left");
						StartCoroutine(Climb ());
					}
				}
			} else if (Input.GetKey (KeyCode.D) && !climbing) {
				if (holdSpawner.sequence [amountClimbed] == HoldSpawner.RIGHT) {
					if (timeHeldRight < holdTimeRight) {
						timeHeldRight += Time.deltaTime;
					} else {
						timeHeldRight = 0;
						animator.SetTrigger ("Right");
						StartCoroutine(Climb ());
					}
				}
			}
		//}
	}

	public IEnumerator Climb() {
		Vector3 hold = holdSpawner.holds [amountClimbed].transform.position;
		Vector3 holdTarget = 
			new Vector3 (
				hold.x,
				hold.y - 3.5f,
				hold.z - 1.5f
			);

		target = holdTarget;
		amountClimbed++;

		climbing = true;
		yield return new WaitForSeconds (0.5f);
		climbing = false;
	}
}