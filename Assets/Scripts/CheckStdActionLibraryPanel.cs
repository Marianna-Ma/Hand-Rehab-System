using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class CheckStdActionLibraryPanel : MonoBehaviour, IPointerClickHandler{

    StandardActionLibrary stdlib = new StandardActionLibrary();
    List<string> selectActList = new List<string>();

    // Use this for initialization
    void Start () {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "AddActionButton")
        {
            //跳转到添加动作界面

            Debug.Log("clickclick");
            string next_id = stdlib.getActionId();
            PlayerPrefs.SetString("newActionID", next_id);
            Debug.Log(next_id);

            GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AddStdActionLibraryPanel");
        }
        if (eventData.pointerPress.name == "DeleteActionButton")
        {
            GameObject.Find("CheckStdActionLibraryPanel").GetComponent<CreateStdActionTable>().getSelectActList(selectActList);
            Debug.Log("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            int able = stdlib.deleteStandardActions(selectActList);
            if(able == 1)
            {
                Messagebox.MessageBox(IntPtr.Zero, "有无效选中动作！", "失败", 0);
            }
            else
            {
                Messagebox.MessageBox(IntPtr.Zero, "删除标准动作成功！", "成功", 0);
            }
        }
        if (eventData.pointerPress.name == "BackButton")
        {
            //跳转到开始界面

            GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminStartPanel");
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
