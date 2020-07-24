using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelMainMenu : MonoBehaviour 
{
	public Text textResponse;
	public Sprite icon;
	public string title;
	public string description;
	public bool[] buttons = new bool[4];

	void Start()
	{
		UpdateResponse ("");
	}

	public void OnClickButton(string buttonType)
	{
		switch (buttonType) {
		case "Show":
			PanelPop.ShowPopUp (icon,title,description,buttons,OnRecieveDialogButtonAction);
			break;
		case "Scene1":
			SceneManager.LoadScene ("Scene1");
			break;
		case "Scene2":
			SceneManager.LoadScene ("Scene2");
			break;
		case "Scene3":
			SceneManager.LoadScene ("Scene3");
			break;
		case "Print":
			Debug.Log (SceneManager.GetActiveScene().name);
			break;
		}
	}

	private void OnRecieveDialogButtonAction(string buttonType)
	{
		switch (buttonType) {
		case "Yes":
			UpdateResponse ("yes");
			break;
		case "No":
			UpdateResponse ("no");
			break;
		case "Close":
			UpdateResponse ("");
			break;
		case "Buy":
			UpdateResponse ("buy");
			break;
		}
	}

	private void UpdateResponse(string response)
	{
		textResponse.text = response;
	}

}
