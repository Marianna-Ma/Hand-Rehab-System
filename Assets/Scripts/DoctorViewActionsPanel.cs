using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorViewActionsPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClickCheckLeftActionButton(){
		string act_id = PlayerPrefs.GetString ("selectStdActionID");
		string act_path = Application.dataPath + "/StandardActionLibrary/" + act_id + "0.json";

		PlayerPrefs.SetString ("actionPath", act_path);
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorViewStdReplayPanel");



	}

	public void ClickCheckRightActionButton(){
		string act_id = PlayerPrefs.GetString ("selectStdActionID");
		string act_path = Application.dataPath + "/StandardActionLibrary/" + act_id + "1.json";

		PlayerPrefs.SetString ("actionPath", act_path);
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorViewStdReplayPanel");
//		GameObject.Find("Canvas/DoctorViewStdActionLibraryPanel/SearchButton").GetComponent<Button>().interactable = true;
//
//		GameObject.Find("Canvas/DoctorViewStdActionLibraryPanel/BackButton").GetComponent<Button>().interactable = true;
	}

}
