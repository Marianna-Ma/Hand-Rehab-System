using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorCheckPlanPanel : MonoBehaviour {

    List<PlanToggleInfo> selectActList = new List<PlanToggleInfo>();
    string selectPatientID;
    public InputField nameInput;

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
        GameObject.Find("DoctorCheckPlanPanelNew").GetComponent<CreatePlanTable>().getSelectActList(selectActList);
        selectPatientID = PlayerPrefs.GetString("selectPatientID");
        Debug.Log("GetToggleInfo:-------------------");
        foreach(PlanToggleInfo tInfo in selectActList)
        {
            Debug.Log(selectPatientID + "," + tInfo.id + "," + tInfo.hand);
            //TrainingPlan.deleteTrainingPlan(selectPatientID, tInfo.id, tInfo.hand);
        }
        GameObject obj = GameObject.Find("Canvas/DoctorCheckPatientPanelNew");
        CreatePatientPanel panel = (CreatePatientPanel)obj.GetComponent(typeof(CreatePatientPanel));
        panel.Start();
    }

    public void ClickSearchButton()
    {
        GameObject planTable = GameObject.Find("Canvas/DoctorCheckPlanPanelNew");
        CreatePlanTable createTable = (CreatePlanTable)planTable.GetComponent(typeof(CreatePlanTable));
        createTable.currentTP = TrainingPlan.searchTrainingPlan(nameInput.text.ToString(), PlayerPrefs.GetString("selectPatientID"));//TODO：获得病人编号和输入框内容
        createTable.clicked = true;
    }
    
}
