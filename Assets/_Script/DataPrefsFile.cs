using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataPrefsFile 
{
//	private string fileName = "Data.dat";

	public void DeleteFile(string fileName)
	{
		if (File.Exists (Application.persistentDataPath + "/" + fileName)) {
			File.Delete (Application.persistentDataPath + "/" + fileName);
		}
	}

	public T LoadData<T>(T inputType,string fileName)
	{
		if(File.Exists(Application.persistentDataPath+"/"+fileName))
		{
			BinaryFormatter bf = null;
			FileStream file = null;
			try
			{
				bf = new BinaryFormatter();
				file = File.Open(Application.persistentDataPath+"/"+fileName,FileMode.Open);
				inputType = (T)bf.Deserialize(file);
			}
			catch(System.Exception e)
			{
				Debug.Log("Exception during LoadData : "+e.Message);
			}
			finally
			{
				if(file != null)
					file.Close();
			}
		}
		else
		{
			Debug.Log("No file found");
		}

		return inputType;
	}
	public void SaveData<T>(T data,string fileName)
	{
//		PlayerData data = new PlayerData();

		FileStream file = null;
		BinaryFormatter bf = null;

		try
		{
			file = File.Create(Application.persistentDataPath+"/"+fileName);
			bf = new BinaryFormatter();
			bf.Serialize(file,data); 
			Debug.Log("Data Saved successfully"+Application.persistentDataPath+"/"+fileName);
		}
		catch(System.Exception e)
		{
			Debug.Log("Exception during LoadData : "+e.Message);	
		}
		finally
		{
			if(file != null)
				file.Close();
		}
	}
}
