using UnityEngine;
using System.Collections;

public class RopeSpawner : MonoBehaviour {

	public GameObject ropeRef;
	public GameObject[] spawnedRopes;

	public int amtToSpawn;
	public float xSpacing;

	void Start () {
		spawnedRopes = new GameObject[amtToSpawn];

		for (int i = 0; i < amtToSpawn; i++) {
			Vector3 newPos = 
				new Vector3 (
					transform.position.x + (i * xSpacing),
					transform.position.y,
					transform.position.z
				);

			//Debug.Log ("new pos: " + newPos);

			spawnedRopes [i] = (GameObject) Instantiate (ropeRef, newPos, transform.rotation);
		}
	}
}
