﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminStartPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //修改密码界面
    public void ClickUpdatePswdButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminUpdatePasswordPanel");
    }
    //跳转到修改个人信息界面
    public void ClickAdminChangeInfoButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminChangeInfoPanel");
    }

    //跳转到医生管理界面
    public void ClickAdminDoctorButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminCheckDoctorPanel");
        GameObject obj = GameObject.Find("Canvas/AdminCheckDoctorPanel");
        CreateDocPanel panel = (CreateDocPanel)obj.GetComponent(typeof(CreateDocPanel));
        panel.Start();
    }

    //跳转到训练计划管理界面
    public void ClickAdminTrainPlanButton()
    {
        //GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminChangeStandardAcitonPanel");
        //GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AddStdActionLibraryPanel");
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("CheckStdActionLibraryPanel");
    }

    //跳转到查看病人信息界面
    public void ClickAdminPatientButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("");
    }

    //退回登录界面
    public void ClickQuitButton()
    {
        //此处是否要注销player？
        PlayerPrefs.DeleteAll();
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("LoginPanel");
    }

}
