using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorCheckPlanPanel : MonoBehaviour{

    List<PlanToggleInfo> selectActList = new List<PlanToggleInfo>();
    string selectPatientID;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        foreach (PlanToggleInfo tInfo in selectActList)
        {
            Debug.Log(selectPatientID + "," + tInfo.id + "," + tInfo.hand);
            //TrainingPlan.deleteTrainingPlan(selectPatientID, tInfo.id, tInfo.hand);
        }
    }
}
