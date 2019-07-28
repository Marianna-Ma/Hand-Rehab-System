using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorCheckPatientPanel : MonoBehaviour {
    public GameObject SmallPanel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickAddPatientButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorAddPatientPanel");
    }

    public void ClickDeletePatientButton()
    {
        //GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorDeletePatientPanel");
        SmallPanel.SetActive(true);
        GameObject.Find("AddPatientButton").GetComponent<Button>().interactable = false;
        GameObject.Find("DeletePatientButton").GetComponent<Button>().interactable = false;
        GameObject.Find("Canvas/DoctorCheckPatientPanelNew/BackButton").GetComponent<Button>().interactable = false;
    }

    public void ClickBackButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorStartPanel");
    }
}
