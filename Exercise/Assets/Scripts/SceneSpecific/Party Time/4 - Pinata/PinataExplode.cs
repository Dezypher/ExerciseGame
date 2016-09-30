using UnityEngine;
using System.Collections;

public class PinataExplode : MonoBehaviour {

	public GameObject explosion;
	public GameLogic gameLogic;

	void Start() {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
	}

	void Update () {
		if (gameLogic.started && gameLogic.done) {
			StartCoroutine (Explode ());
		}
	}

	public IEnumerator Explode() {
		yield return new WaitForSeconds (0.3f);

		Instantiate (explosion, transform.position, transform.rotation);

		Destroy (this.gameObject);
	}
}
