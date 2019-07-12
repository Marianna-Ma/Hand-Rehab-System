using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorViewStdReplayPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string act_path = "";
		if (PlayerPrefs.HasKey ("actionPath")) {
			Debug.Log ("++++++++++++++++++++++++++++++++++++++++++");
			act_path = PlayerPrefs.GetString ("actionPath");
			Debug.Log (act_path);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClickBackButton () {
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorViewStdActionLibraryPanel");
		GameObject.Find("Canvas/DoctorViewStdActionLibraryPanel/SearchButton").GetComponent<Button>().interactable = true;

		GameObject.Find("Canvas/DoctorViewStdActionLibraryPanel/BackButton").GetComponent<Button>().interactable = true;
	}
}
