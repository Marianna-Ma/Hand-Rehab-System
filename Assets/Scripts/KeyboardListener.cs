using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KeyboardListener : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //暂停并询问是否退出
            GameObject.Find("Canvas").GetComponent<HandControl>().InvisiHands();
            //GameObject.Find("PatientTrainPanel").GetComponent<evaluate>().LeaveEvaluation();
            if (EditorUtility.DisplayDialog("退出训练", "您是否要退出训练？", "确认", "取消"))
            {
                GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientCheckPlanPanel");
            }
            else
            {
                GameObject.Find("Canvas").GetComponent<HandControl>().ShowHands();
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Messagebox.MessageBox(IntPtr.Zero, "是否继续？", "暂停中", 0);
        }
    }
}