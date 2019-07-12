using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorStartPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(true);
        Debug.Log("医生初始界面打开了");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //跳转到修改密码界面
    public void ClickUpdatePswdButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorUpdatePasswordPanel");
    }
    //跳转到注册病人界面
    public void ClickPatientButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorCheckPatientPanelNew");
    }

    //跳转到修改个人信息界面
    public void ClickChangeInfoButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorChangeInfoPanel");
    }

    //训练结果窗口
    public void ClickCheckStdLibraryButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorViewStdActionLibraryPanel");
    }

    //返回登录界面
    public void ClickQuitButton()
    {
        PlayerPrefs.DeleteAll();
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("LoginPanel");
    }
}
