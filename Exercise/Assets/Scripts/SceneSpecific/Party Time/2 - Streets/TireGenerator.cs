using UnityEngine;
using System.Collections;

public class TireGenerator : MonoBehaviour {

	public GameObject initialTireGroup;

	public int numTires;
	public GameObject[] tires;
	public int[] positions;

	public int currTire = -1;

	void Start () {
		int sideStreak = 0;
		int lastSide = 0;

		tires = new GameObject[numTires];
		positions = new int[numTires];

		for (int i = 1; i < numTires; i++) {
			int side = Random.Range (0, 2);

			if (side == 2)
				side = 1;

			if (lastSide == side)
				sideStreak++;
			else
				sideStreak = 0;

			if (sideStreak >= 2) {
				switch (side) {
				case 0:
					side = 1;
					break;
				case 1:
					side = 0;
					break;
				}

				sideStreak = 0;
			}

			lastSide = side;

			positions [i] = side;

			Vector3 newPos = initialTireGroup.transform.position;

			newPos.z += 2 * i;

			tires [i] = (GameObject) Instantiate (initialTireGroup, newPos, initialTireGroup.transform.rotation);

			Destroy (tires [i].transform.GetChild (side).gameObject);
		}

		int sided = Random.Range (0, 2);

		if (sided == 2)
			sided = 1;

		tires [0] = initialTireGroup;
		positions [0] = sided;

		Destroy (tires [0].transform.GetChild (sided).gameObject);
	}
}
