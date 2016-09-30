using UnityEngine;
using System.Collections;

public class SpawnIceMonster : MonoBehaviour {

	public int numMonstersLeft;
	public int numMonstersSpawned = 0;

	public Transform spawnPointLeft;
	public Transform spawnPointRight;

	public Transform targetLeft;
	public Transform targetRight;

	public GameObject monsterLeftRef;
	public GameObject monsterRightRef;

	public GameObject monsterLeft;
	public GameObject monsterRight;

	public bool aliveLeft = false;
	public bool aliveRight = false;

	public float spawnTimerLeft;
	public float spawnTimerRight;

	public float intervalMin;
	public float intervalMax;

	public GameObject explosionEffect;
	public GameObject deathEffect;

	public bool effectOn;

	private GameLogic gameLogic;
	private ScoreRecorder scoreRecorder;

	void Start() {
		//Instantiate initial interval

		spawnTimerLeft = Random.Range (0, 3);
		spawnTimerRight = Random.Range (0, 3);

		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		scoreRecorder = GameObject.Find ("ScoreRecorder").GetComponent<ScoreRecorder> ();
	}

	// Update is called once per frame
	void Update () {
		if (gameLogic.started && !gameLogic.done) {
			if (numMonstersSpawned < numMonstersLeft) {
				if (!aliveLeft) {
					if (spawnTimerLeft > 0)
						spawnTimerLeft -= Time.deltaTime;
					else {
						spawnTimerLeft = Random.Range (intervalMin, intervalMax);
						aliveLeft = true;

						monsterLeft = SpawnMonster (monsterLeftRef, spawnPointLeft.position, targetLeft.position);
					}
				}

				if (!aliveRight) {
					if (spawnTimerRight > 0)
						spawnTimerRight -= Time.deltaTime;
					else {
						spawnTimerRight = Random.Range (intervalMin, intervalMax);
						aliveRight = true;

						monsterRight = SpawnMonster (monsterRightRef, spawnPointRight.position, targetRight.position);
					}
				}
			}
		}
	}

	public GameObject SpawnMonster(GameObject reference, Vector3 spawnPoint, Vector3 target) {
		GameObject monster = (GameObject) Instantiate (reference, spawnPoint, reference.transform.rotation);

		if(effectOn)
			Instantiate (explosionEffect, spawnPoint, reference.transform.rotation);

		Vector3 newScale = new Vector3 (0.065f, 0.065f, 0.065f);

		monster.transform.localScale = newScale;

		monster.GetComponent<IceMonster> ().targetNode = target;

		return monster;
	}

	public void Kill(int side) {
		switch (side) {
		case 0:
			if(effectOn)
				Instantiate (explosionEffect, monsterLeft.transform.position, explosionEffect.transform.rotation);
			Destroy (monsterLeft);
			aliveLeft = false;
			break;
		case 1:
			if(effectOn)
				Instantiate (explosionEffect, monsterRight.transform.position, explosionEffect.transform.rotation);
			Destroy (monsterRight);
			aliveRight = false;
			break;
		}
	}
}