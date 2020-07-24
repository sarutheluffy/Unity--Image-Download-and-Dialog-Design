using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelPop : MonoBehaviour 
{
	public static PanelPop Instance;

	public Button[] buttons;
	public RectTransform rtContent;
	public Transform transformImageBG;
	public Image imageIcon;
	public Text textTitle;
	public Text textDescription;

	private static System.Action<string> buttonCallback;

	void Start()
	{
		InitializeAnim ();
	}

	private void InitializeAnim()
	{
		transformImageBG.DORotate (new Vector3 (0, 0, 360), 5f, RotateMode.FastBeyond360).SetLoops (-1, LoopType.Incremental).SetEase (Ease.Linear);
		rtContent.DOAnchorPosY (-(Screen.height + rtContent.rect.height), 2f).From ();
	}

	private void OutAnim(TweenCallback callback)
	{
		rtContent.DOAnchorPosY (-(Screen.height + rtContent.rect.height), 1f).OnComplete (callback).SetEase(Ease.Linear);
	}
		
	public static PanelPop ShowPopUp(Sprite icon,string title,string descrition,bool[] buttons,System.Action<string> buttonCallback)
	{
		if (Instance == null) 
		{
			PanelPop.buttonCallback = buttonCallback;
			GameObject obj = Instantiate (Resources.Load ("PanelPop")) as GameObject;
			Transform tranformCanvas = GameObject.Find("Canvas").transform;
			obj.transform.SetParent (tranformCanvas, false);
			Instance = obj.GetComponent<PanelPop> ();
			Instance.SetData (icon, title, descrition, buttons);
		}
		return Instance;
		
	}

	private void SetData(Sprite icon,string title,string description,bool[] buttons)
	{
		imageIcon.sprite = icon;
		textTitle.text = title;
		textDescription.text = description;
		for (int i = 0; i < buttons.Length; i++) {
			this.buttons [i].gameObject.SetActive (buttons [i]);
		}
	}


	public void OnClickButton(string buttonType)
	{
		buttonCallback (buttonType);
		OutAnim (() => { Destroy(this.gameObject);});
	}
	
}
