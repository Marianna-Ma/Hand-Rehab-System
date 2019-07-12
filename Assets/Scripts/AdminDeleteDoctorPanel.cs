using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using UnityEngine.UI;

public class AdminDeleteDoctorPanel : MonoBehaviour {

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



    public void ClickBackButton()
    {
        SmallPanel.SetActive(false);
        GameObject.Find("AddDoctorButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DeleteDoctorButton").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/AdminCheckDoctorPanel/BackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("AdminCheckDoctorPanel").GetComponent<CreateDocPanel>().Start();
        //GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminStartPanel");
    }
}
