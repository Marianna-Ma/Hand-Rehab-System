using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;

public class DoctorUI : MonoBehaviour, IPointerClickHandler {
	// 修改密码 面板的输入框
	public InputField dc_firstPswdField;
	public InputField dc_secondPswdField;
	// 修改信息 面板的输入框
	public InputField dcNameField;
	public InputField dcSexField;
	public InputField dcProField;
	public InputField dcTeleField;
	// 添加患者 面板的输入框
	public InputField ptIDField;
	public InputField ptNameField;
	public InputField ptSexField;
	public InputField ptTeleField;

    public static string host = "119.3.231.171";                //IP地址
    public static string port = "3306";             //端口号
    public static string userName = "admin";            //用户名
    public static string password = "Rehabsys@2019";            //密码
    public static string databaseName = "rehabsys";     //数据库名称

    public Doctor test;

	// Use this for initialization
	void Start () {
		test = new Doctor(host, port, userName, password, databaseName);
	}
	
	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.pointerPress.name == "updatePswdButton")	//如果当前按下的按钮是修改密码按钮
			test.UpdatePassword (dc_firstPswdField.text.ToString(), dc_secondPswdField.text.ToString());
		if (eventData.pointerPress.name == "changeInfoButton")
			test.ChangeInfo (dcNameField.text.ToString(), dcSexField.text.ToString(), dcProField.text.ToString(), dcTeleField.text.ToString());
		if (eventData.pointerPress.name == "addPatientButton")	//如果当前按下的按钮是添加患者按钮
			test.AddPatient (ptIDField.text.ToString(), ptNameField.text.ToString(), ptSexField.text.ToString(), ptTeleField.text.ToString());
		//if (eventData.pointerPress.name == "deletePatientButton")	//如果当前按下的按钮是删除患者按钮
		//	test.DeletePatient();
        if (eventData.pointerPress.name == "deletePatientButton")
        {
            //如果当前按下的按钮是删除患者按钮
            GameObject table = GameObject.Find("Canvas/DoctorCheckPatientPanel/ScrollView/Viewport/Content");
            List<string> strList = new List<string>();
            Debug.Log("*******************************************");
            foreach (Transform t in table.GetComponentsInChildren<Transform>())
            {
                if (t.name.Contains("patient".ToLower()) && t.Find("Toggle").GetComponent<Toggle>().isOn)
                {
                    strList.Add(t.Find("PatientID").GetComponent<Text>().text);
                    Debug.Log("SelectedPatientID:" + t.Find("PatientID").GetComponent<Text>().text);
                }
            }
            string[] ptID = strList.ToArray();
            //test.DeletePatient(ptID);
        }

    }
}
