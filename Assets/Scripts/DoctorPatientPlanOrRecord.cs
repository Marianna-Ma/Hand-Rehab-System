using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorPatientPlanOrRecord : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("Canvas/DoctorPatientPlanOrRecord").SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClickPlanButton () {
		GameObject.Find ("Canvas/DoctorPatientPlanOrRecord").SetActive (false);
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorCheckPlanPanelNew");
		GameObject.Find ("Canvas/DoctorCheckPlanPanelNew").GetComponent<CreatePlanTable> ().Start ();
	}

	public void ClickRecordButton () {
		GameObject.Find ("Canvas/DoctorPatientPlanOrRecord").SetActive (false);
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorHistoryRecordPanel");
//		GameObject.Find ("Canvas/DoctorHistoryRecordPanel").GetComponent<DoctorUI> ().;
	}

	public void ClickCancelButton () {
		GameObject.Find ("Canvas/DoctorPatientPlanOrRecord").SetActive (false);
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorCheckPatientPanelNew");
		GameObject.Find ("Canvas/DoctorCheckPatientPanelNew").GetComponent<CreatePatientPanel> ().Start ();
	}
}
