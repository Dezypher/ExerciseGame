using UnityEngine;
using System.Collections;

public class ArduinoPort : MonoBehaviour {

	public UnityEngine.UI.InputField input;
	public string arduinoPort;

	void Start () {
		//Should search for port with the Arduino Device
	}

	public void SetArduinoPort() {
		arduinoPort = input.text;
	}
}
