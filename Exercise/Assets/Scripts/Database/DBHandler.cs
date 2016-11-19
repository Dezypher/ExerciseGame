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
	void Start () {    
	}
}
