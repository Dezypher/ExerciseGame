using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class DBHandler : MonoBehaviour {

	string conn;
	IDbConnection dbconn;
	IDataReader reader;
	IDbCommand dbcmd;

	/*
	private void ExecuteQuery(string query) {
		IDbCommand dbcmd = dbconn.CreateCommand();
		dbcmd.CommandText = query;
		while (reader.Read())
		{
			int value = reader.GetInt32(0);
			string name = reader.GetString(1);
			int rand = reader.GetInt32(2);

			Debug.Log( "value= "+value+"  name ="+name+"  random ="+  rand);
		}
	}
	*/

	private void InitializeData() {
		string filepath = Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + "exercise.s3db";

		Debug.Log ("FilePath: " + filepath);

		try {
			if(!System.IO.File.Exists(filepath))
			{
				SqliteConnection.CreateFile(filepath);

				conn = "URI=file:" + filepath;

				OpenDB();

				dbcmd=dbconn.CreateCommand();
				dbcmd.CommandText = 
					"CREATE TABLE resultset( "
				+ "id      INTEGER PRIMARY KEY AUTOINCREMENT, "
				+ "date    STRING, "
				+ "gameid  INTEGER    NOT NULL "
				+ ");"

				+ "CREATE TABLE results(" +
				"resultsetid  INTEGER," +
				"stageid      INTEGER," +
				"score        INTEGER," +
				"totalscore   INTEGER," +
				"time         INTEGER" +
				");" 
				
				+"CREATE TABLE progressreport( "
				+ "id           INTEGER    PRIMARY KEY AUTOINCREMENT, "
				+ "date         STRING,  "
				+ "timeGP       INTEGER,     "
				+ "timeBP       INTEGER,     "
				+ "timeActv     INTEGER,     "
				+ "timeInactv   INTEGER,     "
				+ "timeTotal    INTEGER     "
				+ ");";

				reader = dbcmd.ExecuteReader();

				CloseDB();
			} else {

				conn = "URI=file:" + filepath;

			}

			Debug.Log("Database initialized with no errors!");
			Debug.Log("Database path: " + filepath);
		} catch (Exception ex) {
			Debug.Log (ex.Message);
		}
	}

	private void OpenDB() {
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
	}

	private void CloseDB() {
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
	}

	// Use this for initialization
	void Awake () {    
		InitializeData ();
	}

	public void InsertResults(int gameID, StageResult[] results) {
		try {
			OpenDB();

			DateTime now = DateTime.Now;

			string dateString = now.Year + "-" + now.Month + "-" + now.Day + "-" + now.Hour + "-" + now.Minute;

			dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = "INSERT INTO resultset(date, gameid) VALUES ('"+dateString+"', "+gameID+")";
			reader = dbcmd.ExecuteReader();

			dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = "SELECT MAX(id) FROM resultset";
			reader = dbcmd.ExecuteReader();

			int resultsetid = 0;

			if(reader.Read()) {
				resultsetid = reader.GetInt32(0);
			}

			for(int i = 0; i < results.Length; i++) {
				dbcmd = dbconn.CreateCommand();
				dbcmd.CommandText = "INSERT INTO results(resultsetid, stageid, score, totalscore, time) " +
					"VALUES ("+resultsetid+", "+results[i].stageID+", "+results[i].score+", "+results[i].maxScore+", "+results[i].time+")";
				reader = dbcmd.ExecuteReader();
			}
		} catch (Exception ex) {
			Debug.Log ("Record Results Exception");
			Debug.Log (ex.Message);
		} finally {
			CloseDB ();
		}
	}

	public StageResult[] GetResults(int resultSetID) {
		ArrayList results = new ArrayList ();

		try {
			OpenDB();

			dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = "SELECT stageid, score, totalscore, time FROM results WHERE resultsetid = " + resultSetID;
			reader = dbcmd.ExecuteReader();

			while(reader.Read()) {
				StageResult result = new StageResult();

				result.stageID = reader.GetInt32(0);
				result.score = reader.GetInt32(1);
				result.maxScore = reader.GetInt32(2);
				result.time = reader.GetInt32(3);

				results.Add(result);
			}

		} catch (Exception ex) {
			Debug.Log ("Record Results Exception");
			Debug.Log (ex.Message);
		} finally {
			CloseDB ();
		}

		StageResult[] stageResults = new StageResult[results.Count];

		for (int i = 0; i < results.Count; i++) {
			stageResults [i] = (StageResult) results [i];
		}

		return stageResults;
	}

	public int[] GetResultIDsFromDate(DateTime date) {
		int[] idArray = null;

		try {
			OpenDB();

			ArrayList ids = new ArrayList();

			string dateTime = date.Year + "-" + date.Month + "-" + date.Day + "-" + date.Hour + "-" + date.Minute;

			dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = "SELECT id FROM resultset WHERE date LIKE " +
						         "'" + date.Year + "-" + date.Month + "-" + date.Day + "-%-%'";
			reader = dbcmd.ExecuteReader();

			while(reader.Read()) {
				int id = reader.GetInt32(0);

				ids.Add(id);
			}

			idArray = new int[ids.Count];

			for(int i = 0; i < ids.Count; i++) {
				idArray[i] = (int) ids[i];
			}
		} catch (Exception ex) {
			Debug.Log ("Record Results Exception");
			Debug.Log (ex.Message);
		} finally {
			CloseDB ();
		}

		return idArray;
	}

	public void InsertProgressReport(ProgressReport report) {
		try {
			OpenDB();

			DateTime now = DateTime.Now;

			string dateString = now.Year + "-" + now.Month + "-" + now.Day + "-" + now.Hour + "-" + now.Minute;

			dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = "DELETE IF EXISTS FROM progressreport WHERE date LIKE "
				+ "'" + now.Year + "-" + now.Month + "-" + now.Day + "-%-%'";
			reader = dbcmd.ExecuteReader();

			dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = "INSERT INTO progressreport(date, timeGP, timeBP, timeActv, timeInactv, timeTotal) " +
				"VALUES ('"+report.date+"', "+report.timeGP+", "+report.timeBP+", "+report.timeActv+", "+report.timeInactv+", "+report.timeTotal+")";
			reader = dbcmd.ExecuteReader();
		} catch (Exception ex) {
			Debug.Log ("Record Results Exception");
			Debug.Log (ex.Message);
		} finally {
			CloseDB ();
		}
	}

	public ProgressReport GetProgressReportByDate(DateTime date) {
		ProgressReport result = null;

		try {
			OpenDB();

			DateTime now = DateTime.Now;

			dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = "SELECT * FROM progressreport WHERE date LIKE "
				+ "'"+date.Year+"-"+date.Month+"-"+date.Day+"-%-%'"; 
			reader = dbcmd.ExecuteReader();

			if(reader.Read()) {
				result = new ProgressReport ();

				string[] dateParts = reader.GetString(1).Split("-".ToCharArray(), StringSplitOptions.None);

				result.date = new DateTime
					(
						Int32.Parse(dateParts[0]),
						Int32.Parse(dateParts[1]),
						Int32.Parse(dateParts[2]),
						Int32.Parse(dateParts[3]),
						Int32.Parse(dateParts[4]),
						0
					);
				result.timeGP     = reader.GetInt32(2);
				result.timeBP     = reader.GetInt32(3);
				result.timeActv   = reader.GetInt32(4);
				result.timeInactv = reader.GetInt32(5);
				result.timeTotal  = reader.GetInt32(6);
			}
		} catch (Exception ex) {
			Debug.Log ("Record Results Exception");
			Debug.Log (ex.Message);
		} finally {
			CloseDB ();
		}

		return result;
	}

	public DateTime GetResultDate(int resultSetID) {
		return DateTime.Now;
	}
}