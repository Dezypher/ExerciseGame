using UnityEngine;
using System.Collections;

public class WallDodgeBehaviour : MonoBehaviour {

	public const int SIDE_LEFT = 0;
	public const int SIDE_RIGHT = 1;

	public int side;

	public float xCoordPlayerPos;
	public float xCoordDelete;
	public float speed;

	private bool done = false;
	private PlayerWallDodge player;
	private GameLogic gameLogic;

	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerWallDodge> ();
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();

		if (transform.position.z > 0)
			side = SIDE_LEFT;
		else if (transform.position.z < 0)
			side = SIDE_RIGHT;
	}

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
				if ((player.playerState == PlayerWallDodge.STATE_DODGE_RIGHT && side == SIDE_LEFT) ||
					(player.playerState == PlayerWallDodge.STATE_DODGE_LEFT && side == SIDE_RIGHT)) {
					gameLogic.points++;
				} else {
					player.animator.SetTrigger ("Hit");
				}

				gameLogic.amtDone++;
				done = true;
			}
		}
	}
}