using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;

public class PatientUI : MonoBehaviour, IPointerClickHandler {
	// 修改密码 面板的输入框
	public InputField pt_firstPswdField;
	public InputField pt_secondPswdField;
	// 修改信息 面板的输入框
	public InputField ptNameField;
	public InputField ptSexField;
	public InputField ptTeleField;

    public static string host = "119.3.231.171";                //IP地址
    public static string port = "3306";             //端口号
    public static string userName = "admin";            //用户名
    public static string password = "Rehabsys@2019";            //密码
    public static string databaseName = "rehabsys";     //数据库名称

    public Patient test;

	// Use this for initialization
	void Start () {
		test = new Patient(host, port, userName, password, databaseName);

	}

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.pointerPress.name == "updatePswdButton")	//如果当前按下的按钮是修改密码按钮
			test.UpdatePassword (pt_firstPswdField.text.ToString(), pt_secondPswdField.text.ToString());
		if (eventData.pointerPress.name == "changeInfoButton")
			test.ChangeInfo (ptNameField.text.ToString(), ptSexField.text.ToString(), ptTeleField.text.ToString());
		if (eventData.pointerPress.name == "historyRecordButton")
//			test.HistoryRecord ("20190620", "400001", 0);
			test.SelectRecords("20190621", "20190623");
	}
}
