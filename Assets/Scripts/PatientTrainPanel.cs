using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatientTrainPanel : MonoBehaviour {

<<<<<<< HEAD
    public void OnPointerClick(PointerEventData eventData)
=======
    public GameObject leaphandleft;
    public GameObject leaphandright;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape");
            //TODO:Pause and ask if patient needed to quit
            ClickTrainPauseButton();
        }
	}

    public void ClickTestButton()
>>>>>>> abbc849db63064b5dff7c46af4221efc5ea98d8b
    {
        if (eventData.pointerPress.name == "TrainPauseButton")       //如果当前按下的按钮是注册按钮 
        {
            Messagebox.MessageBox(IntPtr.Zero, "是否继续？", "暂停中", 0);
        }
    }

    public void ClickTrainPauseButton()
    {
        Debug.Log("Pause");
        leaphandleft.SetActive(false);
        leaphandright.SetActive(false);
    }
}
