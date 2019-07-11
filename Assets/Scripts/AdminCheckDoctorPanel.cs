using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminCheckDoctorPanel : MonoBehaviour {
    public GameObject SmallPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickAddDoctorButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminAddDoctorPanel");
    }

    public void ClickDeleteDoctorButton()
    {
        //GameObject.Find("MainCamera/Canvas/AdminDeleteDoctorPanel").SetActive(true);
        SmallPanel.SetActive(true);
        GameObject.Find("AddDoctorButton").GetComponent<Button>().interactable = false;
        GameObject.Find("DeleteDoctorButton").GetComponent<Button>().interactable = false;
        GameObject.Find("BackButton").GetComponent<Button>().interactable = false;
    }

    public void ClickBackButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminStartPanel");
    }
}
