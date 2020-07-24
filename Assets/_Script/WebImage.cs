using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WebImage : MonoBehaviour 
{
	public Image image;
	public string urlImage;
	public bool shouldCache = true;

	private string imageName;
	void Start()
	{
		imageName = Path.GetFileName (@urlImage);
		if ( shouldCache && FileManager.Instance.IsFileExists (imageName)) {
			Debug.Log("FileExit");
			StartCoroutine (LoadImageFromMemory (FileManager.Instance.GetFilePath(imageName)));
		} 
		else
			DownloadImage.Instance.AddRequest (new Request(urlImage,OnRecieveTexture));
	}

	private IEnumerator LoadImageFromMemory(string url)
	{
		WWW www = new WWW (url);
		yield return www;
		image.sprite = ConvertTex2Sprite (www.texture as Texture2D);
	}

	private void OnRecieveTexture(Texture2D tex2D)
	{
		image.sprite = ConvertTex2Sprite (tex2D);
		if(shouldCache)
			FileManager.Instance.SaveTexture (tex2D, imageName);
	}

	private Sprite ConvertTex2Sprite(Texture2D tex2D)
	{
		return Sprite.Create (tex2D, new Rect (0, 0, tex2D.width, tex2D.height), new Vector2 (0.5f, 0.5f));
	}
}
