using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatientTrainPanel : MonoBehaviour, IPointerClickHandler{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "TrainPauseButton")       //如果当前按下的按钮是注册按钮 
        {
            Messagebox.MessageBox(IntPtr.Zero, "是否继续？", "暂停中", 0);
        }
    }

 
}
