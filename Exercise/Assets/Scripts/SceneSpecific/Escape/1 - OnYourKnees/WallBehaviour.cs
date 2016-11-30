using UnityEngine;
using System.Collections;

public class WallBehaviour : MonoBehaviour {

	public float xCoordPlayerPos;
	public float xCoordDelete;
	public float speed;

	private bool done = false;
	private PlayerOnYourKnees player;
	private GameLogic gameLogic;

	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerOnYourKnees> ();
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
	}

	// Update is called once per frame
	void Update () {
		transform.position =
			new Vector3 (
				transform.position.x + (speed * Time.deltaTime),
				transform.position.y,
				transform.position.z
			);

		if (transform.position.x <= xCoordDelete) {
			Destroy (this.gameObject);
		}

		if (!done) {
			if (transform.position.x <= xCoordPlayerPos) {
				if (player.isKneeled) {
					gameLogic.points += 10;
				} else {
					player.animator.SetTrigger ("Hit");
				}

				gameLogic.amtDone++;
				done = true;
			}
		}
	}
}