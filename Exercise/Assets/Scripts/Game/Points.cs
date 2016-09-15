using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour {

	public GameLogic gameLogic;
	public UnityEngine.UI.Text text; 

	void Start () {
		text = GetComponent<UnityEngine.UI.Text> ();
	}

	void Update () {
		text.text = gameLogic.points + " POINTS (" + gameLogic.amtDone + "/" + gameLogic.doAmt + ")";
	}
}
