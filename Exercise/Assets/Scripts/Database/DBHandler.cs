using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class DBHandler : MonoBehaviour {

	string conn = "URI=file:" + Application.dataPath + "Database/exercise.s3db"; //Path to database.
	IDbConnection dbconn;
	IDataReader reader;
	IDbCommand dbcmd;

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

	private void InitializeData() {

		string filepath = Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + "exercise.s3db";


		if(!System.IO.File.Exists(filepath))
		{
			SqliteConnection.CreateFile(filepath);

			conn = "URI=file:" + filepath;

			OpenDB();

			dbcmd=dbconn.CreateCommand();
			dbcmd.CommandText = 
				"CREATE TABLE resultset( "
			+ "id      INT    PRIMARY KEY, "
			+ "date    STRING, "
			+ "gameid  INT    NOT NULL "
			+ ");"

			+ "CREATE TABLE results(" +
			"resultsetid  INT," +
			"stageid   INT," +
			"score        INT," +
			"totalscore   INT," +
			"time         REAL," +
			");";

			reader = dbcmd.ExecuteReader();

			CloseDB();
		} else {

			conn = "URI=file:" + filepath;

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
			dbcmd.CommandText = "SELECT MAX(ID) FROM resultset";
			reader = dbcmd.ExecuteReader();

			int resultsetid = 0;

			if(reader.Read()) {
				resultsetid = reader.GetInt32(0);
			}

			for(int i = 0; i < results.Length; i++) {
				dbcmd = dbconn.CreateCommand();
				dbcmd.CommandText = "INSERT INTO resultset(resultsetid, stageid, score, totalscore, time) " +
					"VALUES ("+resultsetid+", "+results[i].stageID+", "+results[i].score+", "+results[i].maxScore+", "+results[i].time+")";
				reader = dbcmd.ExecuteReader();
			}
		} catch (Exception ex) {
			Debug.Log ("Record Results Exception");
			Debug.Log (ex.StackTrace);
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
				result.time = reader.GetFloat(3);

				results.Add(result);
			}

		} catch (Exception ex) {
			Debug.Log ("Record Results Exception");
			Debug.Log (ex.StackTrace);
		} finally {
			CloseDB ();
		}

		StageResult[] stageResults = new StageResult[results.Count];

		for (int i = 0; i < results.Count; i++) {
			stageResults [i] = (StageResult) results [i];
		}

		return stageResults;
	}

	public DateTime GetResultDate(int resultSetID) {
		return DateTime.Now;
	}
}
