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

    public GameObject SmallPanel;

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
			GameObject.Find("Canvas/AddStdActionLibraryPanel").GetComponent<StdActionLibraryUI>().Start();
//			GameObject.Find ("AddStdActionLibraryPanel").GetComponent<StdActionLibraryUI> ().Start ();
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

    public void ClickDeleteActionButton()
    {
        SmallPanel.SetActive(true);
        GameObject.Find("AddActionButton").GetComponent<Button>().interactable = false;
        GameObject.Find("DeleteActionButton").GetComponent<Button>().interactable = false;
        GameObject.Find("BackButton").GetComponent<Button>().interactable = false;
    }
}
