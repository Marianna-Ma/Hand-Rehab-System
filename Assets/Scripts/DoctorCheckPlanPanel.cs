using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorCheckPlanPanel : MonoBehaviour {

    List<PlanToggleInfo> selectActList = new List<PlanToggleInfo>();
    string selectPatientID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickBackButton()
    {
        //Debug.Log(GameObject.Find("canvas"))
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorCheckPatientPanel");
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
    }

    public void ClickSearchButton()
    {
        GameObject planTable = GameObject.Find("Canvas/DoctorCheckPlanPanelNew");
        CreatePlanTable createTable = (CreatePlanTable)planTable.GetComponent(typeof(CreatePlanTable));
        createTable.currentTP = TrainingPlan.searchTrainingPlan("伸", "300001");//TODO：获得病人编号和输入框内容
        createTable.clicked = true;
    }
}
