using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DoctorDeletePatientPanel : MonoBehaviour {

    public static string host = "119.3.231.171";        //IP地址
    public static string port = "3306";                 //端口号
    public static string userName = "admin";            //用户名
    public static string password = "Rehabsys@2019";    //密码
    public static string databaseName = "rehabsys";		//数据库名称

    //public Admin test;
    public GameObject SmallPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickDeletePatientButton()
    {

        List<string> selectPatientList = new List<string>();
        //GameObject obj = GameObject.Find("Canvas/AdminCheckDoctorPanel");
        //CreateDocPanel panel = (CreateDocPanel)obj.GetComponent(typeof(CreateDocPanel));
        Debug.Log("delete=============================================");
        GameObject.Find("Canvas/DoctorCheckPatientPanelNew").GetComponent<CreatePatientPanel>().getSelectPatientList(selectPatientList);
        foreach (string id in selectPatientList)
        {
            Debug.Log(id);
        }

        Doctor test = new Doctor(host, port, userName, password, databaseName);
        int flag = test.DeletePatient(selectPatientList);
        if (flag == 1)
            Messagebox.MessageBox(IntPtr.Zero, "删除病人失败！", "失败", 0);
        else
            Messagebox.MessageBox(IntPtr.Zero, "删除病人成功！", "成功", 0);

        SmallPanel.SetActive(false);
        GameObject.Find("AddPatientButton").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/DoctorCheckPatientPanelNew/BackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DeletePatientButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DoctorCheckPatientPanelNew").GetComponent<CreatePatientPanel>().Start();
    }

    public void ClickBackButton()
    {
        SmallPanel.SetActive(false);
        GameObject.Find("AddPatientButton").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/DoctorCheckPatientPanelNew/BackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DeletePatientButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DoctorCheckPatientPanelNew").GetComponent<CreatePatientPanel>().Start();
        //GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminStartPanel");
    }
}
