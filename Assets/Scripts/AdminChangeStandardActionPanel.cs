using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AdminChangeStandardActionPanel : MonoBehaviour {
    public int time = 3;
    GameObject obj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        //CountDownThree();
        //GameObject.Find("ChangeCountDownText").SetActive(true);
        //GameObject.Find("ChangeCountDownText").GetComponent<Text>().text = "按下按钮之后，按'S'键开始录制！";
    }

    public void ClickTranscribeButton()
    {
        //Messagebox.MessageBox(IntPtr.Zero, "按'S'键开始录制！", "注意", 0);
        GameObject.Find("ChangeCountDownText").GetComponent<Text>().text = "";
        //记录标准动作
        string hand_type = PlayerPrefs.GetString("handtype");
        string act_id = PlayerPrefs.GetString("newActionID");
        Debug.Log(hand_type + " " + act_id);
        obj = new GameObject();
        obj.AddComponent<StandardActionLibrary>();
        StandardActionLibrary stdlibrary = (StandardActionLibrary)obj.GetComponent(typeof(StandardActionLibrary));
        stdlibrary.saveStandardAction(act_id, hand_type);
    }

}
