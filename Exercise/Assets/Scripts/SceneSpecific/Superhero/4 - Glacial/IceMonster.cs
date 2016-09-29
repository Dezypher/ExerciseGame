using UnityEngine;
using System.Collections;

public class IceMonster : MonoBehaviour {
	
	public Vector3 targetNode;

	public float moveSpeed;
	public bool notInPosition = true;

	public float threshold;

	private Animator animator;
	private bool attackPhase = false;

	public float attackIntervalMin;
	public float attackIntervalMax;

	public float attackTimer;

	private PlayerGlacial player;
	private GameLogic gameLogic;

	void Awake() {
		animator = GetComponent<Animator> ();

		player = GameObject.Find ("Player").GetComponent<PlayerGlacial>();
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
	}

	// Update is called once per frame
	void Update () {
		if (!gameLogic.done && !gameLogic.pauseLogic) {
			if (notInPosition) {
				transform.position = Vector3.MoveTowards (transform.position, targetNode, moveSpeed * Time.deltaTime);

				animator.SetBool ("Running", true);

				if (Vector3.Distance (transform.position, targetNode) < threshold) {
					notInPosition = false;
					animator.SetBool ("Running", false);
					attackPhase = true;

					//Calculate attack time

					attackTimer = Random.Range (attackIntervalMin, attackIntervalMax);
				}
			}

			if (attackPhase) {
				if (attackTimer > 0)
					attackTimer -= Time.deltaTime;
				else {
					StartCoroutine(Attack ());
				}
			}
		}
	}

	public IEnumerator Attack() {
		animator.SetTrigger ("Attack");
		attackTimer = Random.Range (attackIntervalMin, attackIntervalMax);	

		yield return new WaitForSeconds (0.5f);

		player.Damage ();
	}
}
