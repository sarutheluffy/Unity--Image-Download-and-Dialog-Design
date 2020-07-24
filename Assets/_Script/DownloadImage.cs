using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request
{
	public string url;
	public System.Action<Texture2D> callback;
	public Request(string url,System.Action<Texture2D> callback)
	{
		this.url = url;
		this.callback = callback;
	}
}
public class DownloadImage : MonoBehaviour 
{
	private static DownloadImage instance;
	public static DownloadImage Instance
	{
		get
		{
			if(instance == null){
				instance = FindObjectOfType<DownloadImage>();
				if(instance == null)
				{	GameObject obj = new GameObject("DownloadImage");
					instance = obj.AddComponent<DownloadImage>();
				}
			}
			return instance;
		}
	}

	public int maxDownloadLimitAtOneMoment = 3;
	private int currentDownloadCountAtMoment;
	private List<Request> requestStack = new List<Request> ();

	private IEnumerator DownLoadImage(string url,System.Action<Texture2D> callback)
	{
		WWW www = new WWW (url);
		yield return www;
		if (string.IsNullOrEmpty (www.error)) {
			Texture2D tex2D = www.texture as Texture2D;
			if(tex2D != null)
				callback (tex2D);
		}

		currentDownloadCountAtMoment -= 1;
		CheckForDownload ();
	}

	public void AddRequest(Request request)
	{
		Debug.Log("Request");
		requestStack.Add (request);		
		CheckForDownload ();
	}

	private void CheckForDownload()
	{
		if (currentDownloadCountAtMoment < maxDownloadLimitAtOneMoment && requestStack.Count > 0) {
			Request rqst = PopRequest ();
			StartCoroutine (DownLoadImage (rqst.url,rqst.callback));
			currentDownloadCountAtMoment += 1;
		}
	}

	private Request PopRequest()
	{
		int lastIndex = requestStack.Count - 1;
		Request rqst = requestStack [lastIndex];
		requestStack.RemoveAt (lastIndex);
		return rqst;
	}

}
