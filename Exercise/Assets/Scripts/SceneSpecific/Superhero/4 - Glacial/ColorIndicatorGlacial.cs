using UnityEngine;
using System.Collections;

public class ColorIndicatorGlacial : MonoBehaviour {

	public int side;

	public Color colorActive;
	public Color colorInactive;
	public Color colorDisabled;

	private PlayerGlacial playerGlacial;
	private UnityEngine.UI.Image image;
	private SpawnIceMonster spawnIceMonster;

	void Awake() {
		playerGlacial = GameObject.Find ("Player").GetComponent<PlayerGlacial> ();
		spawnIceMonster = GameObject.Find ("Spawner").GetComponent<SpawnIceMonster> ();
		image = GetComponent<UnityEngine.UI.Image> ();
	}

	// Update is called once per frame
	void Update () {
		Color color = new Color ();

		if (side == 0) {
			if (spawnIceMonster.aliveLeft) {
				if (!spawnIceMonster.monsterLeft.GetComponent<IceMonster> ().notInPosition) {
					if (playerGlacial.doingLeft) {
						image.color = colorActive;
					} else {
						image.color = colorInactive;
					}
				}
			} else
				image.color = colorDisabled;
		}

		if (side == 1) {
			if (spawnIceMonster.aliveRight) {
				if (!spawnIceMonster.monsterRight.GetComponent<IceMonster> ().notInPosition) {
					if (playerGlacial.doingRight) {
						image.color = colorActive;
					} else {
						image.color = colorInactive;
					}
				}
			} else
				image.color = colorDisabled;
		}
	}
}
