using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using Mono.Data.Sqlite;
using LitJson;
using System.IO;
using UnityEngine;

public static class DBReaderWriter {

	// Use this for initialization
	private static string databaseFilePath;
	private static string wordTextFilePath;


	static void InsertWord(string word)
	{
		databaseFilePath = "URI=file:" + Application.dataPath + "/Resources/HangManDB.sqlite";
		using (IDbConnection dbConnection = new SqliteConnection (databaseFilePath))
		{
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = string.Format ("INSERT INTO WordsTable(WordList) VALUES ('{0}')", word);
				dbCommand.CommandText = sqlQuery;
				dbCommand.ExecuteScalar ();
				dbConnection.Close ();

			}
		}

	}
	static string ReadFromDB(int columnIndex)
	{
		databaseFilePath = "URI=file:" + Application.dataPath + "/Resources/HangManDB.sqlite";
		using (IDbConnection dbConnection = new SqliteConnection (databaseFilePath))
		{
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = "SELECT * FROM WordsTable";
				dbCommand.CommandText = sqlQuery;
				using (IDataReader reader = dbCommand.ExecuteReader ()) 
				{
					while (reader.Read ()) 
					{
						//Debug.Log (reader.GetString (0));
						return(reader.GetString(columnIndex));
					}
				}
			}
		}
		return("");

	}

	static void ReadFromTextFile()
	{
		string[] s=File.ReadAllLines (Application.dataPath + "/Resources/WordsTextFile.txt");
		foreach (string word in s) {
			InsertWord (word);
		}
		//Debug.Log (s.Length);
	}
	public static int GetColumnLength()
	{
		int i=0;
		databaseFilePath = "URI=file:" + Application.dataPath + "/Resources/HangManDB.sqlite";
		using (IDbConnection dbConnection = new SqliteConnection (databaseFilePath))
		{
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = "SELECT(*) COUNT(WordList) FROM WordsTable";
				dbCommand.CommandText = sqlQuery;
				using (IDataReader reader = dbCommand.ExecuteReader ()) 
				{
					while (reader.Read ()) 
					{
						i=dbConnection.Database.Length;

					}
				}
			}
		}
		return(i);
	}
}
