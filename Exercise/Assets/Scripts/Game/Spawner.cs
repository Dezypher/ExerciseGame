using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject toSpawn;

	public void Spawn() {
		Instantiate (toSpawn, transform.position, transform.rotation);
	}
}