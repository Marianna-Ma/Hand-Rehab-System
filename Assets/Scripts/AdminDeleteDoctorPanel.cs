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

//<<<<<<< HEAD
//    public void ClickDeleteDoctorButton()
//    {
//        
//        List<string> selectActList = new List<string>();
//        //GameObject obj = GameObject.Find("Canvas/AdminCheckDoctorPanel");
//        //CreateDocPanel panel = (CreateDocPanel)obj.GetComponent(typeof(CreateDocPanel));
//        GameObject.Find("Canvas/AdminCheckDoctorPanel").GetComponent<CreateDocPanel>().getSelectActList(selectActList);
//
//        Admin test = new Admin(host, port, userName, password, databaseName);
//        int flag = test.DeleteDoctor(selectActList);
//		if (flag == 1)
//			Messagebox.MessageBox (IntPtr.Zero, "删除医生失败！", "失败", 0);
//		else {
//			Debug.Log ("你为啥不关！");
//			Messagebox.MessageBox (IntPtr.Zero, "删除医生成功！", "成功", 0);
//
//		}
//		Debug.Log ("你关啊！！！");
//		GameObject.Find ("Canvas/AdminDeleteDoctorPanel").SetActive (false);
//        //SmallPanel.SetActive(false);
//        GameObject.Find("AddDoctorButton").GetComponent<Button>().interactable = true;
//        GameObject.Find("DeleteDoctorButton").GetComponent<Button>().interactable = true;
//        GameObject.Find("BackButton").GetComponent<Button>().interactable = true;
//        GameObject.Find("AdminCheckDoctorPanel").GetComponent<CreateDocPanel>().Start();
//    }
//=======
//    public void ClickDeleteDoctorButton()
//    {
//        
//        List<string> selectActList = new List<string>();
//        //GameObject obj = GameObject.Find("Canvas/AdminCheckDoctorPanel");
//        //CreateDocPanel panel = (CreateDocPanel)obj.GetComponent(typeof(CreateDocPanel));
//        GameObject.Find("Canvas/AdminCheckDoctorPanel").GetComponent<CreateDocPanel>().getSelectActList(selectActList);
//
//        Admin test = new Admin(host, port, userName, password, databaseName);
//        int flag = test.DeleteDoctor(selectActList);
//        if (flag == 1)
//            Messagebox.MessageBox(IntPtr.Zero, "删除医生失败！", "失败", 0);
//        else
//            Messagebox.MessageBox(IntPtr.Zero, "删除医生成功！", "成功", 0);
//
//        SmallPanel.SetActive(false);
//        GameObject.Find("AddDoctorButton").GetComponent<Button>().interactable = true;
//        GameObject.Find("DeleteDoctorButton").GetComponent<Button>().interactable = true;
//        GameObject.Find("Canvas/AdminCheckDoctorPanel/BackButton").GetComponent<Button>().interactable = true;
//        GameObject.Find("AdminCheckDoctorPanel").GetComponent<CreateDocPanel>().Start();
//    }
//>>>>>>> e95ab496ce29c3711098d7e32051d445e7cfbd47

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
