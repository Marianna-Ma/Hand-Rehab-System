using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DoctorDeletePlanPanel : MonoBehaviour {

    public static string host = "119.3.231.171";        //IP地址
    public static string port = "3306";                 //端口号
    public static string userName = "admin";            //用户名
    public static string password = "Rehabsys@2019";    //密码
    public static string databaseName = "rehabsys";     //数据库名称

    public GameObject SmallPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickDoctorDeletePlanButton()
    {

        List<PlanToggleInfo> selectActList = new List<PlanToggleInfo>();
        string selectPatientID;

        GameObject.Find("DoctorCheckPlanPanelNew").GetComponent<CreatePlanTable>().getSelectActList(selectActList);
        selectPatientID = PlayerPrefs.GetString("selectPatientID");
        Debug.Log("GetToggleInfo:-------------------");
        foreach (PlanToggleInfo tInfo in selectActList)
        {
            Debug.Log(selectPatientID + "," + tInfo.id + "," + tInfo.hand);
            TrainingPlan.deleteTrainingPlan(selectPatientID, tInfo.id, tInfo.hand);
        }
        Messagebox.MessageBox(IntPtr.Zero, "删除训练计划成功！", "成功", 0);
        

        SmallPanel.SetActive(false);
        GameObject.Find("SearchButton").GetComponent<Button>().interactable = true;
        GameObject.Find("AddPlanButton").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/DoctorCheckPlanPanelNew/BackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DeletePlanButton").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/DoctorCheckPlanPanelNew").GetComponent<CreatePlanTable>().Start();
    }

    public void ClickBackButton()
    {
        SmallPanel.SetActive(false);
        GameObject.Find("SearchButton").GetComponent<Button>().interactable = true;
        GameObject.Find("AddPlanButton").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/DoctorCheckPlanPanelNew/BackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DeletePlanButton").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/DoctorCheckPlanPanelNew").GetComponent<CreatePlanTable>().Start();
        //GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminStartPanel");
    }
}
