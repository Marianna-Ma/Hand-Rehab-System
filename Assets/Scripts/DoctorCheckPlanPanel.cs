using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DoctorCheckPlanPanel : MonoBehaviour {

    List<PlanToggleInfo> selectActList = new List<PlanToggleInfo>();
    string selectPatientID;
    public InputField nameInput;

    public GameObject SmallPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickAddPlanButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorCheckStdActionLibraryPanel");
        GameObject obj = GameObject.Find("Canvas/DoctorCheckStdActionLibraryPanel");
        obj.GetComponent<CreateDocCheckStdLibTable>().Start();
    }

    public void ClickBackButton()
    {
        //Debug.Log(GameObject.Find("canvas"))
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorCheckPatientPanelNew");
        GameObject obj = GameObject.Find("Canvas/DoctorCheckPatientPanelNew");
        obj.GetComponent<CreatePatientPanel>().Start();
    }

    public void ClickDeletePlanButton()
    {
        SmallPanel.SetActive(true);

        GameObject.Find("SearchButton").GetComponent<Button>().interactable = false;
        GameObject.Find("AddPlanButton").GetComponent<Button>().interactable = false;
        GameObject.Find("DeletePlanButton").GetComponent<Button>().interactable = false;
        GameObject.Find("Canvas/DoctorCheckPlanPanelNew/BackButton").GetComponent<Button>().interactable = false;
    }

    public void ClickSearchButton()
    {
        GameObject planTable = GameObject.Find("Canvas/DoctorCheckPlanPanelNew");
        CreatePlanTable createTable = (CreatePlanTable)planTable.GetComponent(typeof(CreatePlanTable));
        createTable.currentTP = TrainingPlan.searchTrainingPlan(nameInput.text.ToString(), PlayerPrefs.GetString("selectPatientID"));//TODO：获得病人编号和输入框内容
        createTable.clicked = true;
    }
    
}
