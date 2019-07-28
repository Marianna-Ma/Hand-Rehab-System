using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorReplayPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string rec_link = "";

		if (PlayerPrefs.HasKey("selectRecordLink")) {
			rec_link = PlayerPrefs.GetString("selectRecordLink");
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClickBackButton () {
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorHistoryRecordPanel");
	}
		
}
