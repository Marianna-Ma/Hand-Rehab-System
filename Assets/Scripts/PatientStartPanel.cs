﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientStartPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//跳转修改密码界面
	public void ClickPatientUpdatePswdButton()
	{
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientUpdatePasswordPanel");
	}

    //跳转去个人信息修改界面
    public void ClickPatientChangeInfoButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientChangeInfoPanel");
    }

    //跳转至计划界面
    public void ClickPatientStartButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientCheckPlanPanel");

    }

    //跳转去查看记录界面
    public void ClickPatientCheckButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientHistoryRecordPanel");
    }

    //跳转至登录界面
    public void ClickQuitButton()
    {
        //注销player
        PlayerPrefs.DeleteAll();
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("LoginPanel");
    }
}
