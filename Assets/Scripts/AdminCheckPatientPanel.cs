﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCheckPatientPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickBackButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminCheckDoctorPanel");
    }
}
