using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;

public class AdminDeleteDoctorPanel : MonoBehaviour {

    public Admin test;
    public GameObject SmallPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickDeleteDoctorButton()
    { 
        List<string> selectActList = new List<string>();
        //GameObject obj = GameObject.Find("Canvas/AdminCheckDoctorPanel");
        //CreateDocPanel panel = (CreateDocPanel)obj.GetComponent(typeof(CreateDocPanel));
        GameObject.Find("Canvas/AdminCheckDoctorPanel").GetComponent<CreateDocPanel>().getSelectActList(selectActList);

        int flag = test.DeleteDoctor(selectActList);
        if (flag == 1)
            Messagebox.MessageBox(IntPtr.Zero, "删除医生失败！", "失败", 0);
        else
            Messagebox.MessageBox(IntPtr.Zero, "删除医生成功！", "成功", 0);

        SmallPanel.SetActive(false);
        GameObject.Find("AdminCheckDoctorPanel").GetComponent<CreateDocPanel>().Start();
    }

    public void ClickBackButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminStartPanel");
    }
}
