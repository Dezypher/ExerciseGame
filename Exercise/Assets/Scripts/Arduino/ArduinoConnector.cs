/* ArduinoConnector by Alan Zucconi
 * http://www.alanzucconi.com/?p=2979
 */
using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;

[Serializable]
public class ExerciseStatus {
	public String exerciseID;
	public float  timeAlive;
}

public class ArduinoConnector : MonoBehaviour {

	private bool reading = false;

	public GameLogic logic;

	/* The serial port where the Arduino is connected. */
	[Tooltip("The serial port where the Arduino is connected")]
	public string port = "COM4";
	/* The baudrate of the serial port. */
	[Tooltip("The baudrate of the serial port")]
	public int baudrate = 9600;

	public float refreshAmt;

	private SerialPort stream;

	public ExerciseStatus[] exercises;

	public void Start() {
		logic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();

		Open ();
	}	

	public void Update() {

		if (!reading) {
			string output = ReadFromArduino (10);
			//if(output != null)
			//		Debug.Log (output);
			reading = false;
		}

		for (int i = 0; i < exercises.Length; i++) {
			if (exercises [i].timeAlive > 0)
				exercises [i].timeAlive -= Time.deltaTime;
			else
				exercises [i].timeAlive = 0;
		}
	}

	public bool ExerciseAlive(string exerciseID) {
		for (int i = 0; i < exercises.Length; i++) {
			if(exercises[i].exerciseID.Equals(exerciseID)){
				if (exercises [i].timeAlive > 0)
					return true;
				else
					return false;
			}
		}

		return false;
	}

	public void RefreshExercise(string exerciseID) {
		for (int i = 0; i < exercises.Length; i++) {
			if(exercises[i].exerciseID.Equals(exerciseID)) {
				exercises [i].timeAlive = refreshAmt;

				Debug.Log ("Found exercise");
			}
		}
	}

	public void SetExercises(string[] exerciseIDs) {
		this.exercises = new ExerciseStatus[exerciseIDs.Length];

		for (int i = 0; i < exercises.Length; i++) {
			exercises [i] = new ExerciseStatus ();
			exercises [i].exerciseID = exerciseIDs [i];
			exercises [i].timeAlive = 0;
		}
	}

	public void Open () {
		// Opens the serial port
		stream = new SerialPort(port, baudrate);
		stream.ReadTimeout = 50;
		stream.Open();
		//this.stream.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
	}

	public void WriteToArduino(string message)
	{
		// Send the request
		stream.WriteLine(message);
		stream.BaseStream.Flush();
	}

	public string ReadFromArduino(int timeout = 0)
	{	
		reading = true;
		stream.ReadTimeout = timeout;
		try
		{	
			String lineRead = stream.ReadLine();

			String[] lines = lineRead.Split(',');

			for(int i = 0; i < lines.Length; i++) {
				RefreshExercise(lines[i]);

				Debug.Log(lines[i]);
			}

			return "";

			/*
			if(logic.exerciseID.Equals(stream.ReadLine())) {
				logic.isDoingExercise = true;
			}
			*/
		}
		catch (TimeoutException)
		{	
			return null;
		}
	}

	public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
	{
		DateTime initialTime = DateTime.Now;
		DateTime nowTime;
		TimeSpan diff = default(TimeSpan);

		Debug.Log ("Asynchronous Reading Started");

		string dataString = null;

		do
		{
			// A single read attempt
			try
			{
				dataString = stream.ReadLine();
				Debug.Log(dataString);
			}
			catch (TimeoutException)
			{
				dataString = null;
			}

			if (dataString != null)
			{
				callback(dataString);
				yield return null;
			} else
				yield return new WaitForSeconds(0.05f);

			nowTime = DateTime.Now;
			diff = nowTime - initialTime;

		} while (diff.Milliseconds < timeout);

		if (fail != null)
			fail();
		yield return null;
	}

	public void Close()
	{
		stream.Close();
	}
}