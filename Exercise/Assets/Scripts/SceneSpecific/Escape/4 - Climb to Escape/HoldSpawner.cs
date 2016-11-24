using UnityEngine;
using System.Collections;

public class HoldSpawner : MonoBehaviour {

	public const int LEFT = 0;
	public const int RIGHT = 1;

	public GameObject holdPrefab;
	public int amountToSpawn;

	public float yMargin;
	public float xMargin;

	public int spawnedLeft = 0;
	public int spawnedRight = 0;

	public GameObject[] holds;
	public int[] sequence;

	void Start () {
		holds = new GameObject[amountToSpawn];
		sequence = new int[amountToSpawn];

		for (int i = 0; i < amountToSpawn; i++) {
			int side = Random.Range (0, 2);

			int xMod = 0;

			if (side == 0) {
				if (spawnedLeft < amountToSpawn / 2) {
					xMod = -1;
					spawnedLeft++;
				} else {
					xMod = 1;
					spawnedRight++;
				}
			} else {
				if (spawnedRight < amountToSpawn / 2) {
					xMod = 1;
					spawnedRight++;
				} else {
					xMod = -1;
					spawnedLeft++;
				}
			}

			Vector3 pos = new Vector3 (
				transform.position.x + (xMargin * xMod),			              
				transform.position.y + (yMargin * i),
				transform.position.z
			);

			GameObject hold = (GameObject) Instantiate (holdPrefab, pos, holdPrefab.transform.rotation);

			if (xMod == -1)
				sequence [i] = LEFT;
			else
				sequence [i] = RIGHT;

			holds [i] = hold;
		}
	}
}