using UnityEngine;
using System.Collections;

public class DualWallSpawner : MonoBehaviour {

	public Spawner leftSpawner;
	public Spawner rightSpawner;

	public float intervalMax;
	public float intervalMin;

	public float currTime;
	public int amtToSpawn;

	private GameLogic gameLogic;

	//To make sure both sides spawn an equal number of walls
	public int leftSpawned = 0;
	public int rightSpawned = 0;

	void Start() {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
	}

	void Update () {
		if (gameLogic.started && !gameLogic.done && amtToSpawn > (leftSpawned + rightSpawned)) {
			currTime -= Time.deltaTime;

			if (currTime <= 0) {
				int side = Random.Range (0, 1);

				if (side == 0) {
					if (leftSpawned < amtToSpawn / 2) {
						leftSpawner.Spawn ();
						leftSpawned++;
					} else {
						rightSpawner.Spawn ();
						rightSpawned++;
					}
				} else {
					if (rightSpawned < amtToSpawn / 2) {
						rightSpawner.Spawn ();
						rightSpawned++;
					} else {
						leftSpawner.Spawn ();
						leftSpawned++;
					}
				}

				currTime = Random.Range (intervalMin, intervalMax);
			}
		}
	}
}