using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;

public static class DBReaderWriter {

	// Use this for initialization
	private static string databaseFilePath;
	private static string wordTextFilePath;


	static void InsertWord(string word)
	{

		string DatabaseName = "HangManDB.db";

		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

		if (!File.Exists(filepath))
		{
		Debug.Log("Database not in Persistent path");
		// if it doesn't ->
		// open StreamingAssets directory and load the db ->

		#if UNITY_ANDROID
		var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
		while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		// then save to Application.persistentDataPath
		File.WriteAllBytes(filepath, loadDb.bytes);
		#elif UNITY_IOS
		var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#elif UNITY_WP8
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);

		#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#endif

		Debug.Log("Database written");
		}

		var dbPath = filepath;
		#endif
		databaseFilePath = "URI=file:" + dbPath;


//		if (Application.platform == RuntimePlatform.Android) 
//		{
//			databaseFilePath = "jar:file://" + Application.streamingAssetsPath + "/HangManDB.db";
//		} 
//		else
//		{
//			databaseFilePath = "URI=file:" + Application.streamingAssetsPath + "/HangManDB.db";
//		}


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
	public static string[] ReadFromDB()
	{
        List<string> words = new List<string>();


		string DatabaseName = "HangManDB.db";

		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

		if (!File.Exists(filepath))
		{
		Debug.Log("Database not in Persistent path");
		// if it doesn't ->
		// open StreamingAssets directory and load the db ->

		#if UNITY_ANDROID
		var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
		while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		// then save to Application.persistentDataPath
		File.WriteAllBytes(filepath, loadDb.bytes);
		#elif UNITY_IOS
		var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#elif UNITY_WP8
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);

		#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#endif

		Debug.Log("Database written");
		}

		var dbPath = filepath;
		#endif
		databaseFilePath = "URI=file:" + dbPath;

//		if (Application.platform == RuntimePlatform.Android) 
//		{
//			databaseFilePath = "jar:file://" + Application.streamingAssetsPath + "/HangManDB.db";
//		} 
//		else
//		{
//			databaseFilePath = "URI=file:" + Application.streamingAssetsPath + "/HangManDB.db";
//		}

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
                        words.Add(reader.GetString(0));
					}
                    dbConnection.Close();
                    reader.Close();
				}
			}
		}
        return (words.ToArray());

	}

	static void ReadFromTextFile()
	{
		string[] s=File.ReadAllLines (Application.persistentDataPath + "/Resources/WordsTextFile.txt");
		foreach (string word in s) {
			InsertWord (word);
		}
		//Debug.Log (s.Length);
	}
	public static int GetColumnLength()
	{
		int i=0;

		string DatabaseName = "HangManDB.db";

		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

		if (!File.Exists(filepath))
		{
		Debug.Log("Database not in Persistent path");
		// if it doesn't ->
		// open StreamingAssets directory and load the db ->

		#if UNITY_ANDROID
		var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
		while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		// then save to Application.persistentDataPath
		File.WriteAllBytes(filepath, loadDb.bytes);
		#elif UNITY_IOS
		var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#elif UNITY_WP8
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);

		#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#endif

		Debug.Log("Database written");
		}

		var dbPath = filepath;
		#endif
		databaseFilePath = "URI=file:" + dbPath;

//		if (Application.platform == RuntimePlatform.Android) 
//		{
//			databaseFilePath = "jar:file://" + Application.streamingAssetsPath + "/HangManDB.db";
//		} 
//		else
//		{
//			databaseFilePath = "URI=file:" + Application.streamingAssetsPath + "/HangManDB.db";
//		}


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
