using UnityEngine;
using System.Collections;

public class WallSpawner : MonoBehaviour {

	public GameObject wall;
	public float intervalMin;
	public float intervalMax;

	public float timeTillNext = 0;

	public int numToSpawn;

	public int numSpawned;

	void Start () { 
		timeTillNext = Random.Range (intervalMin, intervalMax);
	}
	
	// Update is called once per frame
	void Update () {
		if (numSpawned < numToSpawn) {
			timeTillNext -= Time.deltaTime;

			if (timeTillNext <= 0) {
				Instantiate (wall, transform.position, transform.rotation);
				numSpawned++;
				timeTillNext = Random.Range (intervalMin, intervalMax);
			}
		}
	}
}