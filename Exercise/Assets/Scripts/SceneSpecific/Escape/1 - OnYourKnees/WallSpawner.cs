using UnityEngine;
using System.Collections;

public class WallSpawner : MonoBehaviour {

	public GameObject wall;
	public float intervalMin;
	public float intervalMax;

	public float timeTillNext = 0;

	public int numToSpawn;

	public int numSpawned;

	private GameLogic gameLogic;

	void Start () { 
		timeTillNext = 0;
		gameLogic = GameObject.Find ("GameHandler").GetComponent <GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameLogic.started) {
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
}