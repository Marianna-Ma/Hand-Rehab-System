using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour {
    public GameObject leftHand;
    public GameObject rightHand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowHands()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        Debug.Log("双手显示");
    }

    public void InvisiHands()
    {
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        Debug.Log("双手显示");
    }
}
