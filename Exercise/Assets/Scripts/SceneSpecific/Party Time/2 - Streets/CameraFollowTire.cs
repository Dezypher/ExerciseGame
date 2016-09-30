using UnityEngine;
using System.Collections;

public class CameraFollowTire : MonoBehaviour {

	public Transform player;

	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, player.position.z - 1);
	}
}
