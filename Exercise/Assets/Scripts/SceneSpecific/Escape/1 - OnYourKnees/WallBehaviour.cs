using UnityEngine;
using System.Collections;

public class WallBehaviour : MonoBehaviour {

	public float xCoordPlayerPos;
	public float xCoordDelete;
	public float speed;
	
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
	}
}