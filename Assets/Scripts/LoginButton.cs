﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        //Debug.Log(GameObject.Find("canvas"))
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientStartPanel");
    }
}
