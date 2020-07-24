using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class TextureSavedDate
{
	public string fileName;
	public System.DateTime createdDate = new System.DateTime();
}

[System.Serializable]
public class DataSaved
{
	public List<TextureSavedDate> textureSaveDataList = new List<TextureSavedDate>();
}
public class FileManager : MonoBehaviour
{
	public static FileManager Instance;
	private int invalidateDataAfter = 7;

	private DataPrefsFile dataPrefsFile = new DataPrefsFile();
	public List<TextureSavedDate> textureSaveDateList = new List<TextureSavedDate>();


	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	void OnEnable()
	{
		textureSaveDateList = dataPrefsFile.LoadData(new DataSaved(), "Data.dat").textureSaveDataList;
		for (int i = textureSaveDateList.Count - 1; i >= 0; i--)
		{
			if (System.DateTime.Now.Subtract(textureSaveDateList[i].createdDate).TotalDays > invalidateDataAfter)
			{
				string filePath = Application.dataPath + "/" + textureSaveDateList[i].fileName;
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
				textureSaveDateList.RemoveAt(i);
			}
		}
	}
	void OnDisable()
	{
		DataSaved ds = new DataSaved();
		ds.textureSaveDataList = textureSaveDateList;
		dataPrefsFile.SaveData(ds, "Data.dat");
	}
	private int CountsWay(int[] S, int m, int n)
	{
		int[] table = new int[n + 1];
		table[0] = 1;

		for (int i = 0; i < m; i++)
			for (int j = S[i]; j <= n; j++)
				table[j] += table[j - S[i]];

		return table[n];
	}
	public bool IsFileExists(string fileName)
	{
		return File.Exists(Application.dataPath + "/" + fileName);
	}
	public void SaveTexture(Texture2D tex2D, string fileName)
	{
		byte[] textureBuffer = tex2D.EncodeToPNG();
		File.WriteAllBytes(Application.dataPath + "/" + fileName, textureBuffer);

		TextureSavedDate tsd = new TextureSavedDate();
		tsd.fileName = fileName;
		tsd.createdDate = System.DateTime.Now;
		textureSaveDateList.Add(tsd);
		//File.SetCreationTime(Application.dataPath + "/" + fileName,System.DateTime.Now);
		//not working
	}
	public string GetFilePath(string fileName)
	{
		return "file://" + Application.dataPath + "/" + fileName;
	}
}


