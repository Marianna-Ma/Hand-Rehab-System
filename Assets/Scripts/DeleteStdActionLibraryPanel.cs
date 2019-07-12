using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class DeleteStdActionLibraryPanel : MonoBehaviour {

    StandardActionLibrary stdlib = new StandardActionLibrary();
    //List<string> selectActList = new List<string>();
    public GameObject SmallPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickDeleteStdActionLibraryButton()
    {
        Debug.Log("======================================");
        List<string> selectActList = new List<string>();
        GameObject.Find("CheckStdActionLibraryPanel").GetComponent<CreateStdActionTable>().getSelectActList(selectActList);
        foreach(string id in selectActList)
        {
            Debug.Log(id);
        }
        //Debug.Log("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
        int able = stdlib.deleteStandardActions(selectActList);

        if (able == 1)
        {
            Messagebox.MessageBox(IntPtr.Zero, "有无效选中动作！", "失败", 0);
        }
        else
        {
            Messagebox.MessageBox(IntPtr.Zero, "删除标准动作成功！", "成功", 0);
        }

        SmallPanel.SetActive(false);
        GameObject.Find("AddActionButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DeleteActionButton").GetComponent<Button>().interactable = true;
        GameObject.Find("BackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("CheckStdActionLibraryPanel").GetComponent<CreateStdActionTable>().Start();
    }

    public void ClickBackButton()
    {
        SmallPanel.SetActive(false);
        GameObject.Find("AddActionButton").GetComponent<Button>().interactable = true;
        GameObject.Find("DeleteActionButton").GetComponent<Button>().interactable = true;
        GameObject.Find("BackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("CheckStdActionLibraryPanel").GetComponent<CreateStdActionTable>().Start();
    }
}
