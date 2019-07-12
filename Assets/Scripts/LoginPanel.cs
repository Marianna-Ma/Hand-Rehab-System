using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(true);
        //GameObject.Find("LeapControlledHands/leftHand").SetActive(false);
        //GameObject.Find("LeapControlledHands/rightHand").SetActive(false);
        //GameObject.Find("Canvas").GetComponent<HandControl>.
        GameObject.Find("Canvas").GetComponent<HandControl>().InvisiHands();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickLoginButton()
    {
        //医生验证函数
        //病人验证函数
		GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminStartPanel");
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorStartPanel");
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientStartPanel");
    }
}
